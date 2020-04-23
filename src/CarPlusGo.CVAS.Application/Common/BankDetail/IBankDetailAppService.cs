using Abp.Application.Services;
using CarPlusGo.CVAS.Common.Dto;

namespace CarPlusGo.CVAS.Common
{
    public interface IBankDetailAppService
         : IAsyncCrudAppService<BankDetailDto, long, PagedBankDetailResultRequestDto, BankDetailDto, BankDetailDto>
    {
    }
}
