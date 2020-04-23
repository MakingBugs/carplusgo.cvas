using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using CarPlusGo.CVAS.Car.Dto;
using CarPlusGo.CVAS.Common.Dto;
using CarPlusGo.CVAS.Insure.Enum;
using System;

namespace CarPlusGo.CVAS.Insure.Dto
{
    [AutoMap(typeof(InsurancePolicy))]
    public class InsurancePolicyDto : FullAuditedEntityDto<long>
    {
        /// <summary>
        /// 车籍Id
        /// </summary>
        public long CarBaseId { get; set; }
        public CarBaseDto CarBase { get; set; }
        /// <summary>
        /// 厂商Id
        /// </summary>
        public long SupplierId { get; set; }
        public SupplierDto Supplier { get; set; }
        /// <summary>
        /// 保险合同类型
        /// </summary>
        public InsuranceContractType InsuranceContractType { get; set; }
        /// <summary>
        /// 保单类别
        /// </summary>
        public InsurancePolicyType InsurancePolicyType { get; set; }
        /// <summary>
        /// 保险单号
        /// </summary>
        public string InsuranceNum { get; set; }
        /// <summary>
        /// 保险开始日期
        /// </summary>
        public DateTime StartTime { get; set; }
        /// <summary>
        /// 保险结束日期
        /// </summary>
        public DateTime EndTime { get; set; }
        /// <summary>
        /// 交强险返点
        /// </summary>
        public float CompulsoryInsuranceRebateRate { get; set; }
        /// <summary>
        /// 商业险返点
        /// </summary>
        public float CommercialInsuranceRebateRate { get; set; }
        /// <summary>
        /// 额外返点（包含交强险、商业险）
        /// </summary>
        public float ExtraRebateRate { get; set; }
        /// <summary>
        /// 承运人责任险返点
        /// </summary>
        public float CarrierLiabilityInsuranceRebateRate { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public InsurancePolicyStatus InsurancePolicyStatus { get; set; }
    }
}
