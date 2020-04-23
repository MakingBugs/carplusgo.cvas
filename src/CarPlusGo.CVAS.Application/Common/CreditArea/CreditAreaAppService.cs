using Abp.Application.Services;
using Abp.Domain.Repositories;
using System.Linq;
using Abp.Linq.Extensions;
using CarPlusGo.CVAS.Common.Dto;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;

namespace CarPlusGo.CVAS.Common
{
    public class CreditAreaAppService
        : AsyncCrudAppService<CreditArea, CreditAreaDto, long, PagedCreditAreaResultRequestDto, CreditAreaDto, CreditAreaDto>, ICreditAreaAppService
    {
        public CreditAreaAppService(IRepository<CreditArea, long> repository)
            : base(repository)
        {
        }
        protected override IQueryable<CreditArea> CreateFilteredQuery(PagedCreditAreaResultRequestDto input)
        {
            return Repository.GetAll().WhereIf(input.CityCode.HasValue, x => x.CityId == input.CityCode);
        }

        [RemoteService(false)]
        public override Task Delete(EntityDto<long> input)
        {
            return null;
        }
        [RemoteService(false)]
        public override Task<CreditAreaDto> Get(EntityDto<long> input)
        {
            return null;
        }
        [RemoteService(false)]
        public override Task<CreditAreaDto> Create(CreditAreaDto input)
        {
            return null;
        }

        [RemoteService(false)]
        public override Task<CreditAreaDto> Update(CreditAreaDto input)
        {
            return null;
        }
    }
}