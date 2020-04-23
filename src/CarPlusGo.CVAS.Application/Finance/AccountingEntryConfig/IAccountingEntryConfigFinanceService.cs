using Abp.Application.Services;
using CarPlusGo.CVAS.Finance.Dto;

namespace CarPlusGo.CVAS.Finance
{
    public interface IAccountingEntryConfigAppService
        : IAsyncCrudAppService<AccountingEntryConfigDto, long, PagedAccountingEntryConfigResultRequestDto, CreateOrUpdateAccountingEntryConfigDto, CreateOrUpdateAccountingEntryConfigDto>
    {
    }
}
