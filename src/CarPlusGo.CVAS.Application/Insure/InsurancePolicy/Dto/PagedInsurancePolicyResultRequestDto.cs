using Abp.Application.Services.Dto;
using CarPlusGo.CVAS.Insure.Enum;
using System;

namespace CarPlusGo.CVAS.Insure.Dto
{
    public class PagedInsurancePolicyResultRequestDto : PagedResultRequestDto
    {
        public long[] CarBaseIds { get; set; }
        public string Keyword { get; set; }

        public long? SupplierId { get; set; }
        /// <summary>
        /// 保险合同类型
        /// </summary>
        public InsuranceContractType? InsuranceContractType { get; set; }
        /// <summary>
        /// 保单类别
        /// </summary>
        public InsurancePolicyType? InsurancePolicyType { get; set; }
        /// <summary>
        /// 保单状态
        /// </summary>
        public InsurancePolicyStatus? InsurancePolicyStatus { get; set; }
        public DateTime? StartTimeFrom { get; set; }
        public DateTime? StartTimeTo { get; set; }
        public DateTime? EndTimeFrom { get; set; }
        public DateTime? EndTimeTo { get; set; }
    }
}
