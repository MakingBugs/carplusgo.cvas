using Abp.Domain.Repositories;
using CarPlusGo.CVAS.Finance.Enum;
using CarPlusGo.CVAS.Finance.Handler.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarPlusGo.CVAS.Finance.Handler
{
    public class VehicleLicensingFeesAccountingEntryHandler : DefaultAccountingEntryHandler
    {
        public VehicleLicensingFeesAccountingEntryHandler(string accountingEntryName, IRepository<AccountingEntryConfig, long> accountingEntryConfigRepository, IRepository<AccountingTitle, long> accountingTitleRepository)
            : base(accountingEntryName, accountingEntryConfigRepository, accountingTitleRepository)
        {
        }

        public override Task<Queue<AccountingEntry>> CreateAccountingEntries(decimal amount, CurrencyType currencyType, Dictionary<string, string> keyValuePairs)
        {
            var accountingEntryQueue = new Queue<AccountingEntry>();
            //Todo
            return Task.FromResult(accountingEntryQueue);
        }
    }
}
