using CarPlusGo.CVAS.Common.Dto;
using Abp.Domain.Repositories;
using Abp.Application.Services;
using System.Linq;
using Abp.Linq.Extensions;

namespace CarPlusGo.CVAS.Common
{
    public class CustomerAppService
        : AsyncCrudAppService<Customer, CustomerDto, long, PagedCustomerResultRequestDto, CustomerDto, CustomerDto>, ICustomerAppService
    {
        public CustomerAppService(IRepository<Customer, long> repository)
            : base(repository)
        {
        }
        protected override IQueryable<Customer> CreateFilteredQuery(PagedCustomerResultRequestDto input)
        {
            return Repository.GetAllIncluding()
                .WhereIf(input.TradeItemAuto.HasValue, x => x.TradeItemAuto == input.TradeItemAuto);
        }
    }
}
