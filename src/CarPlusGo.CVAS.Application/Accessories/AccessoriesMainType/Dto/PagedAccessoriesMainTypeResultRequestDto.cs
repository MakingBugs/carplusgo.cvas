using Abp.Application.Services.Dto;

namespace CarPlusGo.CVAS.Accessories.Dto
{
    public class PagedAccessoriesMainTypeResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
    }
}
