using Abp.Application.Services;
using Abp.Domain.Repositories;
using System.Linq;
using Abp.Linq.Extensions;
using CarPlusGo.CVAS.Common.Dto;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;

namespace CarPlusGo.CVAS.Common
{
    public class CreditCityAppService
        : AsyncCrudAppService<CreditCity, CreditCityDto, long, PagedCreditCityResultRequestDto, CreditCityDto, CreditCityDto>, ICreditCityAppService
    {
        public CreditCityAppService(IRepository<CreditCity, long> repository)
            : base(repository)
        {
        }
        protected override IQueryable<CreditCity> CreateFilteredQuery(PagedCreditCityResultRequestDto input)
        {
            return Repository.GetAll().WhereIf(input.ProvinceCode.HasValue, x => x.ProvinceId==input.ProvinceCode);
        }

        [RemoteService(false)]
        public override Task Delete(EntityDto<long> input)
        {
            return null;
        }
        [RemoteService(false)]
        public override Task<CreditCityDto> Get(EntityDto<long> input)
        {
            return null;
        }
        [RemoteService(false)]
        public override Task<CreditCityDto> Create(CreditCityDto input)
        {
            return null;
        }

        [RemoteService(false)]
        public override Task<CreditCityDto> Update(CreditCityDto input)
        {
            return null;
        }
    }
}