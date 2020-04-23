using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using CarPlusGo.CVAS.Finance.Dto;
using System.Linq;

namespace CarPlusGo.CVAS.Finance.AccountTitle
{
    public class AccountingTitleAppService
        : AsyncCrudAppService<AccountingTitle, AccountingTitleDto, long, PagedAccountingTitleResultRequestDto, AccountingTitleDto, AccountingTitleDto>, IAccountingTitleAppService
    {
        public AccountingTitleAppService(IRepository<AccountingTitle, long> repository)
            : base(repository)
        {
        }

        protected override IQueryable<AccountingTitle> CreateFilteredQuery(PagedAccountingTitleResultRequestDto input)
        {
            return Repository.GetAll()
                .WhereIf(!input.Keyword.IsNullOrWhiteSpace(), x => x.Name.Contains(input.Keyword) || x.Code.Contains(input.Keyword))
                .WhereIf(input.IsActive.HasValue, x => x.IsActive == input.IsActive)
                .WhereIf(input.From.HasValue && input.To.HasValue, x => x.CreationTime >= input.From && x.CreationTime <= input.To);
        }

    }
}
