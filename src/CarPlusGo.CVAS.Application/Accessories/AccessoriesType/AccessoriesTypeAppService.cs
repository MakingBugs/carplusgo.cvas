using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using CarPlusGo.CVAS.Accessories.Dto;
using System.Linq;
using System.Threading.Tasks;
using Abp.Extensions;
using Abp.Linq.Extensions;

namespace CarPlusGo.CVAS.Accessories
{
    public class AccessoriesTypeAppService
        : AsyncCrudAppService<AccessoriesType, AccessoriesTypeDto, long, PagedAccessoriesTypeResultRequestDto, CreateOrUpdateAccessoriesTypeDto, CreateOrUpdateAccessoriesTypeDto>, IAccessoriesTypeAppService
    {
        public AccessoriesTypeAppService(IRepository<AccessoriesType, long> repository)
            : base(repository)
        {
        }

        protected override IQueryable<AccessoriesType> CreateFilteredQuery(PagedAccessoriesTypeResultRequestDto input)
        {
            return Repository.GetAllIncluding(x => x.AccessoriesMainType)
                .WhereIf(!input.Keyword.IsNullOrWhiteSpace(), x => x.AccessoriesTypeName.Contains(input.Keyword));
        }

        [RemoteService(false)]
        public override Task Delete(EntityDto<long> input)
        {
            return new Task(() => { });
        }
    }
}
