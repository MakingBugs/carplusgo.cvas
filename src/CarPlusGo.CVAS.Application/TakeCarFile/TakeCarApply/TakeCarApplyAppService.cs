using System;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services;
using CarPlusGo.CVAS.TakeCarFile.Dto;
using Abp.Domain.Repositories;
using System.Linq;
using Abp.Linq.Extensions;
using CarPlusGo.CVAS.Authorization.Users;
using CarPlusGo.CVAS.Common;
using CarPlusGo.CVAS.LocationFile;
using Abp.Application.Services.Dto;
using CarPlusGo.CVAS.Public;
using System.Threading.Tasks;
using Abp.Extensions;

namespace CarPlusGo.CVAS.TakeCarFile
{
    public class TakeCarApplyAppService : AsyncCrudAppService<TakeCarApply, TakeCarApplyDto, long, TakeCarApplyResultRequestDto, TakeCarApplyDto, TakeCarApplyDto>, ITakeCarApplyAppService
    {
        private readonly UserManager _userManager;
        private readonly IRepository<VEmp, long> _VEmp;
        private readonly IRepository<LocationManager, long> _LoctionManager;
        private readonly IRepository<RepositoryManager, long> _RepositoryManager;
        public TakeCarApplyAppService(IRepository<TakeCarApply, long> repository, UserManager userManager, IRepository<VEmp, long> VEmp, IRepository<LocationManager, long> LocationManager, IRepository<RepositoryManager, long> RepositoryManager) : base(repository)
        {
            _userManager = userManager;
            _VEmp = VEmp;
            _LoctionManager = LocationManager;
            _RepositoryManager = RepositoryManager;
        }
        protected override IQueryable<TakeCarApply> CreateFilteredQuery(TakeCarApplyResultRequestDto input)
        {
            List<long> a = new List<long> { 0, 5, 10, 15, 20, 30 };
            List<long> b = new List<long> { 0, 5, 10, 20 };
            return Repository.GetAllIncluding(x => x.Location, x => x.FactoryBrand, x => x.BrandData, x => x.ClasenData, x => x.ItemTypeCode, x => x.ItemStatusCode)
                .Where(x => x.IsDeleted == false)
                .WhereIf(input.TakeDateForm != null && input.TakeDateTo != null, x => input.TakeDateForm.Value.ToLocalTime().Date <= x.CreationTime && x.CreationTime <= input.TakeDateTo.Value.ToLocalTime().ToDayEnd())
                .WhereIf(input.Status.HasValue, x => x.Status == input.Status)
                .WhereIf((!input.IsStatus.HasValue) || input.IsStatus == false, x => b.Contains(x.Status.Value))
                .WhereIf(input.IsStatus.HasValue && input.IsStatus == true, x => a.Contains(x.Status.Value));
        }
        public override async Task<PagedResultDto<TakeCarApplyDto>> GetAll(TakeCarApplyResultRequestDto input)
        {
            CheckGetAllPermission();
            IQueryable<TakeCarApply> query;

            var AdminData = await Fun.GetPermission(_userManager, _VEmp, _LoctionManager, _RepositoryManager, AbpSession.UserId.Value);//设置区域权限

            query = CreateFilteredQuery(input)
                .Where(x => AdminData.areaIdlist.Contains(x.AreaID));

            var totalCount = await AsyncQueryableExecuter.CountAsync(query);

            query = ApplySorting(query, input);
            query = ApplyPaging(query, input);

            var entities = await AsyncQueryableExecuter.ToListAsync(query);

            return new PagedResultDto<TakeCarApplyDto>(
                totalCount,
                entities.Select(MapToEntityDto).ToList()
            );
        }
    }
}
