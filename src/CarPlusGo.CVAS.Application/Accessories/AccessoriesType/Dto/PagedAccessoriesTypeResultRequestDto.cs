using Abp.Application.Services.Dto;

namespace CarPlusGo.CVAS.Accessories.Dto
{
    public class PagedAccessoriesTypeResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
    }
}
