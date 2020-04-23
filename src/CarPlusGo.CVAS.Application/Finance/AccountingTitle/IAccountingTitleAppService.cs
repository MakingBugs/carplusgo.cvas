using Abp.Application.Services;
using CarPlusGo.CVAS.Finance.Dto;

namespace CarPlusGo.CVAS.Finance
{
    public interface IAccountingTitleAppService
        : IAsyncCrudAppService<AccountingTitleDto, long, PagedAccountingTitleResultRequestDto, AccountingTitleDto, AccountingTitleDto>
    {
    }
}
