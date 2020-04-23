using Abp.Application.Services.Dto;

namespace CarPlusGo.CVAS.Common.Dto
{
    public class PagedCreditProvinceResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }

        public bool? IsActive { get; set; }
    }
}
