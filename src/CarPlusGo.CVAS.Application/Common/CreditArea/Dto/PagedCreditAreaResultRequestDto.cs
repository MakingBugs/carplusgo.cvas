using Abp.Application.Services.Dto;

namespace CarPlusGo.CVAS.Common.Dto
{
    public class PagedCreditAreaResultRequestDto : PagedResultRequestDto
    {
        public long? CityCode { get; set; }

        public bool? IsActive { get; set; }
    }
}
