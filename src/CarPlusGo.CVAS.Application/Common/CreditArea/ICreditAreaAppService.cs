using Abp.Application.Services;
using CarPlusGo.CVAS.Common.Dto;

namespace CarPlusGo.CVAS.Common
{
    public interface ICreditAreaAppService
        : IAsyncCrudAppService<CreditAreaDto, long, PagedCreditAreaResultRequestDto, CreditAreaDto, CreditAreaDto>
    {
    }
}