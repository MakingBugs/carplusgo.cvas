using Abp.Dependency;
using CarPlusGo.CVAS.Finance.Enum;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarPlusGo.CVAS.Finance.Handler.Base
{
    public interface IAccountingEntryHandler : ITransientDependency
    {
        Task<Queue<AccountingEntry>> CreateAccountingEntries(decimal amount, CurrencyType currencyType, Dictionary<string, string> keyValuePairs);
    }
}
