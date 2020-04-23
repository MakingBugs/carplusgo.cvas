using Abp.Application.Services.Dto;

namespace CarPlusGo.CVAS.Common.Dto
{
    public class PagedCreditCityResultRequestDto : PagedResultRequestDto
    {
        public long? ProvinceCode { get; set; }

        public bool? IsActive { get; set; }
    }
}
