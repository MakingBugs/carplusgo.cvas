using Abp.Application.Services.Dto;

namespace CarPlusGo.CVAS.Accessories.Dto
{
    public class PagedAccessoriesTsResultRequestDto : PagedResultRequestDto
    {
        public long? AccessoriesTypeAuto { get; set; }
        public long? Supplier { get; set; }
        public long? AccessoriesMainTypeAuto { get; set; }
    }
}
