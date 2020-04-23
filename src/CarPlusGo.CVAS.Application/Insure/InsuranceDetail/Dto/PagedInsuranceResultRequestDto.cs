using Abp.Application.Services.Dto;
using System.Collections.Generic;

namespace CarPlusGo.CVAS.Insure.Dto
{
    public class PagedInsuranceDetailResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
        public List<long> InsurancePolicyIds { get; set; }
    }
}
