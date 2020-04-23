using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using CarPlusGo.CVAS.Finance.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarPlusGo.CVAS.Finance
{
    /// <summary>
    /// 会计科目
    /// </summary>
    [Table("AccountingTitle")]
    public class AccountingTitle : FullAuditedEntity<long>, IMayHaveTenant, IPassivable
    {
        /// <summary>
        /// 会计要素类别
        /// </summary>
        public AccountingElementType AccountingElementType { get; set; }

        /// <summary>
        /// 科目名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 科目编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 科目级次
        /// </summary>
        public int Level { get; set; }

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
