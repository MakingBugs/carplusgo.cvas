using Abp.Application.Services.Dto;

namespace CarPlusGo.CVAS.Car.Dto
{
    public class PagedFactoryBrandResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }

        public bool? IsActive { get; set; }
    }
}
