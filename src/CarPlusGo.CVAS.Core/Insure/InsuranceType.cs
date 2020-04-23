using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarPlusGo.CVAS.Insure
{
    /// <summary>
    /// 险种
    /// </summary>
    [Table("InsuranceType")]
    public class InsuranceType : FullAuditedEntity<long>, IPassivable
    {
        /// <summary>
        /// 险种名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// Is this user active?
        /// If as user is not active, he/she can not use the application.
        /// </summary>
        public virtual bool IsActive { get; set; }
    }
}
