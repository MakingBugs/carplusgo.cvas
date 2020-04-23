using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using CarPlusGo.CVAS.Common.Dto;
using CarPlusGo.CVAS.Insure.Enum;
using System.Collections.Generic;

namespace CarPlusGo.CVAS.Insure.Dto
{
    [AutoMap(typeof(InsurancePreset))]
    public class InsurancePresetDto : FullAuditedEntityDto<long>
    {
        public string Name { get; set; }
        public long SupplierId { get; set; }
        /// <summary>
        /// 厂商
        /// </summary>
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
        /// 预设保险种类
        /// </summary>
        public List<long> PresetInsuranceType { get; set; }
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
