using Abp.Dependency;
using Abp.Domain.Repositories;
using CarPlusGo.CVAS.Finance.Handler.Base;

namespace CarPlusGo.CVAS.Finance.Handler
{
    /// <summary>
    /// 会计分录工厂类
    /// </summary>
    public class AccountingEntryHandlerFactory : ITransientDependency
    {
        private readonly IRepository<AccountingEntryConfig, long> _accountingEntryConfigRepository;

        private readonly IRepository<AccountingTitle, long> _accountingTitleRepository;

        public AccountingEntryHandlerFactory(IRepository<AccountingEntryConfig, long> accountingEntryConfigRepository, IRepository<AccountingTitle, long> accountingTitleRepository)
        {
            _accountingEntryConfigRepository = accountingEntryConfigRepository;
            _accountingTitleRepository = accountingTitleRepository;
        }

        public virtual IAccountingEntryHandler Create(string accountingEntryName)        {            IAccountingEntryHandler accountingEntryHandler = null;            switch (accountingEntryName)
            {
                case "OA暂借":
                    accountingEntryHandler = new OANormalAccountingEntryHandler(accountingEntryName, _accountingEntryConfigRepository, _accountingTitleRepository);
                    break;
                case "车辆上牌费销账":
                    accountingEntryHandler = new VehicleLicensingFeesAccountingEntryHandler(accountingEntryName, _accountingEntryConfigRepository, _accountingTitleRepository);
                    break;
            }
            return accountingEntryHandler;
        }
    }
}
