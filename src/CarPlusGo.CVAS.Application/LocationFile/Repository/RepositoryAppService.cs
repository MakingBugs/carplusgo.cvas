using Abp.Application.Services;
using Abp.Domain.Repositories;
using CarPlusGo.CVAS.LocationFile.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Abp.Linq.Extensions;
using CarPlusGo.CVAS.Authorization.Users;
using CarPlusGo.CVAS.Common;
using Abp.Application.Services.Dto;
using System.Threading.Tasks;
using CarPlusGo.CVAS.Public;

namespace CarPlusGo.CVAS.LocationFile
{
    public class RepositoryAppService: 
        AsyncCrudAppService<Repository, RepositoryDto, long, RepositoryResultRequestDto, RepositoryDto, RepositoryDto>, IRepositoryAppService
    {
        private readonly UserManager _userManager;
        private readonly IRepository<VEmp, long> _VEmp;
        private readonly IRepository<LocationManager, long> _LoctionManager;
        private readonly IRepository<RepositoryManager, long> _RepositoryManager;
        public RepositoryAppService(IRepository<Repository, long> repository, UserManager userManager, IRepository<VEmp,long> VEmp, IRepository<LocationManager, long> LocationManager, IRepository<RepositoryManager,long> RepositoryManager)
           : base(repository)
        {
            _userManager = userManager;
            _VEmp = VEmp;
            _LoctionManager = LocationManager;
            _RepositoryManager = RepositoryManager;
        }
        protected override IQueryable<Repository> CreateFilteredQuery(RepositoryResultRequestDto input)
        {
            return Repository.GetAllIncluding(x => x.Location)
                .Where(x => x.IsDeleted == false)
                .WhereIf(input.RepositoryType.HasValue, x => x.RepositoryType == input.RepositoryType)
                .WhereIf(input.RepositoryName != null && input.RepositoryName != "", x => x.RepositoryName.Contains(input.RepositoryName))
                .WhereIf(input.AreaID.HasValue, x => x.AreaID == input.AreaID)
                .WhereIf(input.IsStop.HasValue, x => x.IsStop == input.IsStop);

        }
        public override async Task<PagedResultDto<RepositoryDto>> GetAll(RepositoryResultRequestDto input)
        {
            CheckGetAllPermission();
            IQueryable<Repository> query;

            var AdminData = await Fun.GetPermission(_userManager, _VEmp, _LoctionManager, _RepositoryManager, AbpSession.UserId.Value);//设置仓库权限

            query = CreateFilteredQuery(input)
                .Where(x => AdminData.areaIdlist.Contains(x.AreaID));


            var totalCount = await AsyncQueryableExecuter.CountAsync(query);

            query = ApplySorting(query, input);
            query = ApplyPaging(query, input);

            var entities = await AsyncQueryableExecuter.ToListAsync(query);

            return new PagedResultDto<RepositoryDto>(
                totalCount,
                entities.Select(MapToEntityDto).ToList()
            );
        }
    }
}
