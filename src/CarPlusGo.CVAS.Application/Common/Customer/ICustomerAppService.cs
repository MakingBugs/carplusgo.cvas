using Abp.Application.Services;
using CarPlusGo.CVAS.Common.Dto;

namespace CarPlusGo.CVAS.Common
{
    public interface ICustomerAppService
    : IAsyncCrudAppService<CustomerDto, long, PagedCustomerResultRequestDto, CustomerDto, CustomerDto>
    {
    }
}