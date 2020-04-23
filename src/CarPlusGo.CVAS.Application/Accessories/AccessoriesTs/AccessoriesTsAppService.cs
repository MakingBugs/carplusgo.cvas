using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using CarPlusGo.CVAS.Accessories.Dto;
using System.Linq;
using System.Threading.Tasks;
using Abp.Linq.Extensions;

namespace CarPlusGo.CVAS.Accessories
{
    public class AccessoriesTsAppService
        : AsyncCrudAppService<AccessoriesTs, AccessoriesTsDto, long, PagedAccessoriesTsResultRequestDto, CreateOrUpdateAccessoriesTsDto, CreateOrUpdateAccessoriesTsDto>, IAccessoriesTsAppService
    {
        public AccessoriesTsAppService(IRepository<AccessoriesTs, long> repository)
            : base(repository)
        {
        }

        protected override IQueryable<AccessoriesTs> CreateFilteredQuery(PagedAccessoriesTsResultRequestDto input)
        {
            return Repository.GetAllIncluding(x => x.AccessoriesType, x => x.AccessoriesType.AccessoriesMainType, x => x.ItemCode)
                .WhereIf(input.AccessoriesMainTypeAuto.HasValue, x => x.AccessoriesType.AccessoriesMainTypeAuto.Equals(input.AccessoriesMainTypeAuto))
                .WhereIf(input.AccessoriesTypeAuto.HasValue, x => x.AccessoriesTypeAuto.Equals(input.AccessoriesTypeAuto))
                .WhereIf(input.Supplier.HasValue, x => x.Supplier.Equals(input.Supplier) && x.ItemType == 883);
        }

        [RemoteService(false)]
        public override Task Delete(EntityDto<long> input)
        {
            return new Task(() => { });
        }
    }
}
