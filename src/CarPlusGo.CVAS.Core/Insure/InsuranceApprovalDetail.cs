using Abp.Domain.Entities.Auditing;
using CarPlusGo.CVAS.Insure.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarPlusGo.CVAS.Insure
{
    /// <summary>
    /// 保险签核明细
    /// </summary>
    [Table("InsuranceApprovalDetail")]
    public class InsuranceApprovalDetail : FullAuditedEntity<long>
    {
        [ForeignKey("InsuranceApproval")]
        public long InsuranceApprovalId { get; set; }
        /// <summary>
        /// 保险签核
        /// </summary>
        public InsuranceApproval InsuranceApproval { get; set; }
        /// <summary>
        /// 保单
        /// </summary>
        [ForeignKey("InsurancePolicy")]
        public long InsurancePolicyId { get; set; }
        public InsurancePolicy InsurancePolicy { get; set; }
        /// <summary>
        /// 保险单号
        /// </summary>
        public string InsuranceNum { get; set; }
        /// <summary>
        /// 车牌号码
        /// </summary>
        public string MakNo { get; set; }
        /// <summary>
        /// 保单类别
        /// </summary>
        public InsurancePolicyType InsurancePolicyType { get; set; }
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
        /// 客户名称
        /// </summary>
        public string CusName { get; set; }
    }
}
