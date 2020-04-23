using Abp.Application.Services;
using CarPlusGo.CVAS.Common.Dto;

namespace CarPlusGo.CVAS.Common
{
    public interface IBankTypeAppService
        : IAsyncCrudAppService<BankTypeDto, int, PagedBankTypeResultRequestDto, BankTypeDto, BankTypeDto>
    {
    }
}
