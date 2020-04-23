using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace CarPlusGo.CVAS.Insure.Dto
{
    [AutoMap(typeof(InsuranceDetail))]
    public class InsuranceDetailDto : FullAuditedEntityDto<long>
    {
        /// <summary>
        /// 险种Id
        /// </summary>
        public long InsuranceTypeId { get; set; }
        public InsuranceTypeDto InsuranceType { get; set; }
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
        public long InsurancePolicyId { get; set; }
        /// <summary>
        /// 保单
        /// </summary>
        public InsurancePolicyDto InsurancePolicy { get; set; }
    }
}
