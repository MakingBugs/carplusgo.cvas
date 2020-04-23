using Abp.Application.Services.Dto;

namespace CarPlusGo.CVAS.Common.Dto
{
    public class PagedItemCodeResultRequestDto : PagedResultRequestDto
    {
        public int[] ItemTypes { get; set; }
        public string ItemName { get; set; }
    }
}
