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
    public class AccessoriesMainTypeAppService
        : AsyncCrudAppService<AccessoriesMainType, AccessoriesMainTypeDto, long, PagedAccessoriesMainTypeResultRequestDto, AccessoriesMainTypeDto, AccessoriesMainTypeDto>, IAccessoriesMainTypeAppService
    {
        public AccessoriesMainTypeAppService(IRepository<AccessoriesMainType, long> repository)
            : base(repository)
        {
        }

        protected override IQueryable<AccessoriesMainType> CreateFilteredQuery(PagedAccessoriesMainTypeResultRequestDto input)
        {
            return Repository.GetAll()
                .WhereIf(!input.Keyword.IsNullOrWhiteSpace(), x => x.AccessoriesMainName.Contains(input.Keyword));
        }

        [RemoteService(false)]
        public override Task Delete(EntityDto<long> input)
        {
            return new Task(() => { });
        }
    }
}
