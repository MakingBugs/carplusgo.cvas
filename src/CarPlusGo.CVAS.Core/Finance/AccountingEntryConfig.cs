using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using CarPlusGo.CVAS.Finance.Enum;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarPlusGo.CVAS.Finance
{
    /// <summary>
    /// 会计分录配置
    /// </summary>
    [Table("AccountingEntryConfig")]
    public class AccountingEntryConfig:FullAuditedEntity<long>, IMayHaveTenant, IPassivable
    {
        /// <summary>
        /// 配置名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 上级标识
        /// </summary>
        public long? ParentId { get; set; }

        /// <summary>
        /// 是否为主配置
        /// </summary>
        public bool IsMaster { get; set; }

        /// <summary>
        /// 会计分录配置明细
        /// </summary>
        [ForeignKey("ParentId")]
        public virtual List<AccountingEntryConfig> Children { get; set; }

        /// <summary>
        /// 会计要素改变方向
        /// </summary>
        public ElementChangeDirection? ElementChangeDirection { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int? Sort { get; set; }

        [ForeignKey("AccountingTitle")]
        public long? AccountingTitleId { get; set; }

        /// <summary>
        /// 会计科目
        /// </summary>
        public AccountingTitle AccountingTitle { get; set; }

        /// <summary>
        /// Tenant Id of this user.
        /// </summary>
        public virtual int? TenantId { get; set; }

        /// <summary>
        /// Is this user active?
        /// If as user is not active, he/she can not use the application.
        /// </summary>
        public virtual bool IsActive { get; set; }
    }
}
