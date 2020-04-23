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
    public class LocationAppService:
        AsyncCrudAppService<Location, LocationDto, long, LocationResultRequestDto, LocationDto, LocationDto>, ILocationAppService
    {
        private readonly UserManager _userManager;
        private readonly IRepository<VEmp, long> _VEmp;
        private readonly IRepository<LocationManager, long> _LoctionManager;
        private readonly IRepository<RepositoryManager, long> _RepositoryManager;
        public LocationAppService(IRepository<Location, long> repository, UserManager userManager, IRepository<VEmp, long> VEmp, IRepository<LocationManager, long> LocationManager,IRepository<RepositoryManager, long> RepositoryManager) 
            : base(repository)
        {
            _userManager = userManager;
            _VEmp = VEmp;
            _LoctionManager = LocationManager;
            _RepositoryManager = RepositoryManager;
        }
        protected override IQueryable<Location> CreateFilteredQuery(LocationResultRequestDto input)
        {
            return Repository.GetAll()
                .Where(x => x.IsDeleted == false)
                .WhereIf(input.AreaName != null && input.AreaName != "", x => x.AreaName.Contains(input.AreaName))
                .WhereIf(input.IsStop.HasValue, x => x.IsStop == input.IsStop);

        }
        //public override async Task<PagedResultDto<LocationDto>> GetAll(LocationResultRequestDto input)
        //{
        //    CheckGetAllPermission();
        //    IQueryable<Location> query;

        //    var AdminData = await Fun.GetPermission(_userManager, _VEmp, _LoctionManager, _RepositoryManager, AbpSession.UserId.Value);//设置区域权限

        //    query = CreateFilteredQuery(input)
        //        .Where(x => AdminData.areaIdlist.Contains(x.Id));

        //    var totalCount = await AsyncQueryableExecuter.CountAsync(query);

        //    query = ApplySorting(query, input);
        //    query = ApplyPaging(query, input);

        //    var entities = await AsyncQueryableExecuter.ToListAsync(query);

        //    return new PagedResultDto<LocationDto>(
        //        totalCount,
        //        entities.Select(MapToEntityDto).ToList()
        //    );
        //}
    }
}
