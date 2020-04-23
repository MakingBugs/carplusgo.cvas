using Abp.Application.Services;
using Abp.Domain.Repositories;
using CarPlusGo.CVAS.Finance.Dto;
using CarPlusGo.CVAS.Finance.Handler;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarPlusGo.CVAS.Finance
{
    public class AccountingEntryAppService
        : AsyncCrudAppService<AccountingEntry, AccountingEntryDto, long, PagedAccountingEntryResultRequestDto, AccountingEntryDto, AccountingEntryDto>, IAccountingEntryAppService

    {
        private readonly IRepository<AccountingTitle, long> _accountingTitleRepository;
        private readonly IRepository<AccountingEntryConfig, long> _accountingEntryConfigRepository;
        public AccountingEntryAppService(IRepository<AccountingEntry, long> repository, IRepository<AccountingEntryConfig, long> accountingEntryConfigRepository, IRepository<AccountingTitle, long> accountingTitleRepository)
            : base(repository)
        {
            _accountingTitleRepository = accountingTitleRepository;
            _accountingEntryConfigRepository = accountingEntryConfigRepository;
        }

        public virtual async Task<List<AccountingEntryDto>> CreateAll(CreateAllAccountingEntryDto input)
        {
            CheckCreatePermission();

            var accountingEntryDtos = new List<AccountingEntryDto>();

            var accountingEntryHandler = new AccountingEntryHandlerFactory(_accountingEntryConfigRepository, _accountingTitleRepository).Create(input.AccountingEntryName);

            var keyValuePairs = new Dictionary<string, string>
            {
                { "Description", input.Description }
            };

            var accountingEntries = await accountingEntryHandler.CreateAccountingEntries(input.Amount, input.CurrencyType, keyValuePairs);


            while (accountingEntries.TryDequeue(out AccountingEntry accountingEntry))
            {
                Repository.Insert(accountingEntry);
                accountingEntryDtos.Add(MapToEntityDto(accountingEntry));
            }

            return accountingEntryDtos;
        }

    }
}
