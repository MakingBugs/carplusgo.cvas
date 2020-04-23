using Abp.Application.Services.Dto;

namespace CarPlusGo.CVAS.Car.Dto
{
    public class PagedBrandResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }

        public bool? IsActive { get; set; }
    }
}
