using Abp.Application.Services;
using CarPlusGo.CVAS.Common.Dto;

namespace CarPlusGo.CVAS.Common
{
    public interface IAccBankAppService
        : IAsyncCrudAppService<AccBankDto, long, PagedAccBankResultRequestDto, AccBankDto, AccBankDto>
    {
    }
}
