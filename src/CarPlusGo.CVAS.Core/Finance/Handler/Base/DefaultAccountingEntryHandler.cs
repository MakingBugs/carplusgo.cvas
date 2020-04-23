using Abp;
using Abp.Domain.Repositories;
using CarPlusGo.CVAS.Finance.Enum;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarPlusGo.CVAS.Finance.Handler.Base
{
    /// <summary>
    /// 处理会计分录程序
    /// </summary>
    public abstract class DefaultAccountingEntryHandler : IAccountingEntryHandler
    {
        /// <summary>
        /// 会计分录配置
        /// </summary>
        protected AccountingEntryConfig _accountingEntryConfig;

        protected DefaultAccountingEntryHandler(string accountingEntryName, IRepository<AccountingEntryConfig, long> accountingEntryConfigRepository, IRepository<AccountingTitle, long> accountingTitleRepository)
        {
            _accountingEntryConfig = accountingEntryConfigRepository.GetAllIncluding(x => x.Children)
                .FirstOrDefault(x => x.Name == accountingEntryName && x.IsActive && !x.IsDeleted);
            if (_accountingEntryConfig == null)
            {
                throw new AbpException($"会计分录枚举值：{accountingEntryName}所对应会计分录主配置不存在");
            }

            if (_accountingEntryConfig.Children != null && _accountingEntryConfig.Children.Count > 0)
            {
                //按Sort字段排序
                _accountingEntryConfig.Children.Sort((a, b) => a.Sort.GetValueOrDefault(0) - b.Sort.GetValueOrDefault(0));

                var accountingTitles = accountingTitleRepository.GetAllList(x => x.IsActive && !x.IsDeleted);
                if (accountingTitles != null && accountingTitles.Count > 0)
                {
                    SetChildrenAccountingTitle(_accountingEntryConfig.Children, accountingTitles);
                }
                else
                {
                    throw new AbpException($"会计科目配置列表为空");
                }
            }
            else
            {
                throw new AbpException($"会计分录枚举值：{accountingEntryName}所对应会计分录子配置不存在");
            }
        }

        /// <summary>
        /// 设置子分录会计科目
        /// </summary>
        private void SetChildrenAccountingTitle(List<AccountingEntryConfig> accountingEntryConfigs, List<AccountingTitle> accountingTitles)
        {
            foreach (var accountingEntryConfig in accountingEntryConfigs)
            {
                if (!accountingEntryConfig.IsMaster)
                {
                    accountingEntryConfig.AccountingTitle = accountingTitles.FirstOrDefault(x => x.Id == accountingEntryConfig.AccountingTitleId);
                }

                if (accountingEntryConfig.Children != null && accountingEntryConfig.Children.Count > 0)
                {
                    SetChildrenAccountingTitle(accountingEntryConfig.Children.ToList(), accountingTitles);
                }
            }
        }

        /// <summary>
        /// 创建会计分录
        /// </summary>
        public virtual Task<Queue<AccountingEntry>> CreateAccountingEntries(decimal amount, CurrencyType currencyType, Dictionary<string, string> keyValuePairs)
        {
            var accountingEntryQueue = new Queue<AccountingEntry>();

            _accountingEntryConfig.Children.ForEach(x =>
            {
                accountingEntryQueue.Enqueue(new AccountingEntry(keyValuePairs.GetValueOrDefault("Description"), x.AccountingTitle, currencyType, x.ElementChangeDirection, amount, _accountingEntryConfig.TenantId));
            });

            return Task.FromResult(accountingEntryQueue);
        }
    }
}
