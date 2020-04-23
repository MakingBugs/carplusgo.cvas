using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using CarPlusGo.CVAS.Insure.Enum;

namespace CarPlusGo.CVAS.Insure.Dto
{
    [AutoMap(typeof(InsuranceDetail))]
    public class UpdateInsuranceDetailDto : FullAuditedEntityDto<long>
    {
        /// <summary>
        /// 险种Id
        /// </summary>
        public long InsuranceTypeId { get; set; }
        /// <summary>
        /// 操作类别
        /// </summary>
        public InsuranceOperationType InsuranceOperationType { get; set; }
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
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 保单Id
        /// </summary>
        public long InsurancePolicyId { get; set; }
        /// <summary>
        /// 批单号
        /// </summary>
        public string SerialNumber { get; set; }
    }
}
