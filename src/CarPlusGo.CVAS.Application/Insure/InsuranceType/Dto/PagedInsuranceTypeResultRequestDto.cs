using Abp.Application.Services.Dto;

namespace CarPlusGo.CVAS.Insure.Dto
{
    public class PagedInsuranceTypeResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }

        public bool? IsActive { get; set; }
    }
}
