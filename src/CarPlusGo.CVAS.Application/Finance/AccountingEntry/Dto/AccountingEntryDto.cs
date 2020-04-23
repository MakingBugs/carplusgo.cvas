using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using CarPlusGo.CVAS.Finance.Enum;

namespace CarPlusGo.CVAS.Finance.Dto
{
    /// <summary>
    /// 会计分录
    /// </summary>
    [AutoMap(typeof(AccountingEntry))]
    public class AccountingEntryDto:CreationAuditedEntityDto<long>
    {
        /// <summary>
        /// 凭证号
        /// </summary>
        public int VoucherNumber { get; set; }

        /// <summary>
        /// 摘要
        /// </summary>
        public string Description { get; set; }

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
