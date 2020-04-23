using Abp.Application.Services.Dto;
using CarPlusGo.CVAS.Insure.Enum;

namespace CarPlusGo.CVAS.Insure.Dto
{
    public class PagedInsurancePresetResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }

        public bool? IsActive { get; set; }

        public long? SupplierId { get; set; }
        /// <summary>
        /// 保险合同类型
        /// </summary>
        public InsuranceContractType? InsuranceContractType { get; set; }
        /// <summary>
        /// 保单类别
        /// </summary>
        public InsurancePolicyType? InsurancePolicyType { get; set; }

        public long? Num { get; set; }
    }
}
