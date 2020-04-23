using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarPlusGo.CVAS.Insure
{
    /// <summary>
    /// 保单明细
    /// </summary>
    [Table("InsuranceDetail")]
    public class InsuranceDetail : FullAuditedEntity<long>
    {
        /// <summary>
        /// 险种Id
        /// </summary>
        [ForeignKey("InsuranceType")]
        public long InsuranceTypeId { get; set; }
        public InsuranceType InsuranceType { get; set; }
        /// <summary>
        /// 保险金额
        /// </summary>
        public decimal InsuredAmount { get; set; }
        /// <summary>
        /// 原价保费
        /// </summary>
        public decimal OriginalAmount { get; set; }
        /// <summary>
        /// 签单保费
        /// </summary>
        public decimal TransactionAmount { get; set; }
        /// <summary>
        /// 不计免赔原价保费
        /// </summary>
        public decimal NoDeductibleOriginalAmount { get; set; }
        /// <summary>
        /// 不计免赔签单保费
        /// </summary>
        public decimal NoDeductibleTransactionAmount { get; set; }
        /// <summary>
        /// 返点金额
        /// </summary>
        public decimal RebateAmount { get; set; }
        /// <summary>
        /// 额外返点金额
        /// </summary>
        public decimal ExtraRebateAmount { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        [ForeignKey("InsurancePolicy")]
        public long InsurancePolicyId { get; set; }
        /// <summary>
        /// 保单
        /// </summary>
        public InsurancePolicy InsurancePolicy { get; set; }
    }
}
