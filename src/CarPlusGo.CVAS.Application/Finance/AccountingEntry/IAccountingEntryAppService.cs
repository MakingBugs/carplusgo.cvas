using Abp.Application.Services;
using CarPlusGo.CVAS.Finance.Dto;

namespace CarPlusGo.CVAS.Finance
{
    public interface IAccountingEntryAppService
        : IAsyncCrudAppService<AccountingEntryDto, long, PagedAccountingEntryResultRequestDto, AccountingEntryDto, AccountingEntryDto>
    {
    }
}
