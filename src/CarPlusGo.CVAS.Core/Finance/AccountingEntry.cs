using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using CarPlusGo.CVAS.Finance.Enum;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarPlusGo.CVAS.Finance
{
    /// <summary>
    /// 会计分录
    /// </summary>
    [Table("AccountingEntry")]
    public class AccountingEntry : CreationAuditedEntity<long>, IMayHaveTenant
    {

        public AccountingEntry()
            : base()
        {
        }

        public AccountingEntry(string description, AccountingTitle accountingTitle, CurrencyType currencyType, ElementChangeDirection? elementChangeDirection, decimal amount, int? tenantId)
            : this()
        {
            Description = description;
            AccountingTitle = accountingTitle;
            AccountingTitleId = accountingTitle.Id;
            CurrencyType = currencyType;
            TenantId = tenantId;
            switch (accountingTitle.AccountingElementType)
            {
                case AccountingElementType.Asset:
                    BalanceDirectionType = elementChangeDirection == ElementChangeDirection.Increase ? BalanceDirection.Debit : BalanceDirection.Credit;
                    Amount = Math.Abs(amount);
                    break;
                case AccountingElementType.Liability:
                case AccountingElementType.OwnersEquity:
                    BalanceDirectionType = elementChangeDirection == ElementChangeDirection.Increase ? BalanceDirection.Credit : BalanceDirection.Debit;
                    Amount = Math.Abs(amount);
                    break;
                case AccountingElementType.Revenue:
                    BalanceDirectionType = BalanceDirection.Credit;
                    Amount = elementChangeDirection == ElementChangeDirection.Increase ? Math.Abs(amount): -Math.Abs(amount);
                    break;
                case AccountingElementType.Expense:
                case AccountingElementType.Cost:
                    BalanceDirectionType = BalanceDirection.Debit;
                    Amount = elementChangeDirection == ElementChangeDirection.Increase ? Math.Abs(amount) : -Math.Abs(amount);
                    break;
            }
        }

        public AccountingEntry(int voucherNumber, string description, AccountingTitle accountingTitle, CurrencyType currencyType, ElementChangeDirection elementChangeDirection, decimal amount, int? tenantId)
            : this(description, accountingTitle, currencyType, elementChangeDirection, amount, tenantId)
        {
            VoucherNumber = voucherNumber;
        }

        /// <summary>
        /// 凭证号
        /// </summary>
        public int VoucherNumber { get; set; }

        /// <summary>
        /// 摘要
        /// </summary>
        public string Description { get; set; }

        [ForeignKey("AccountingTitle")]
        public long AccountingTitleId { get; set; }

        /// <summary>
        /// 会计科目
        /// </summary>
        public AccountingTitle AccountingTitle { get; set; }

        /// <summary>
        /// 币别
        /// </summary>
        public CurrencyType CurrencyType { get; set; }

        /// <summary>
        /// 余额方向
        /// </summary>
        public BalanceDirection BalanceDirectionType { get; set; }

        /// <summary>
        /// 金额
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// 导入金蝶状态 0 未导入 1 已导入
        /// </summary>
        public int EasStatus { get; set; }

        /// <summary>
        /// Tenant Id of this user.
        /// </summary>
        public virtual int? TenantId { get; set; }
    }
}
