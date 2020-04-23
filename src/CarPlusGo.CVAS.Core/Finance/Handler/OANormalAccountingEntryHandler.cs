using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using CarPlusGo.CVAS.Finance.Enum;
using CarPlusGo.CVAS.Finance.Handler.Base;

namespace CarPlusGo.CVAS.Finance.Handler
{
    public class OANormalAccountingEntryHandler : DefaultAccountingEntryHandler
    {
        public OANormalAccountingEntryHandler(string accountingEntryName, IRepository<AccountingEntryConfig, long> accountingEntryConfigRepository, IRepository<AccountingTitle, long> accountingTitleRepository)
            : base(accountingEntryName, accountingEntryConfigRepository, accountingTitleRepository)
        {
        }

        public override async Task<Queue<AccountingEntry>> CreateAccountingEntries(decimal amount, CurrencyType currencyType, Dictionary<string, string> keyValuePairs)
        {
            return await base.CreateAccountingEntries(amount, currencyType, keyValuePairs);
        }
    }
}
