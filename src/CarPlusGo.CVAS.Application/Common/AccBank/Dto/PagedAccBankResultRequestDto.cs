using Abp.Application.Services.Dto;

namespace CarPlusGo.CVAS.Common.Dto
{
    public class PagedAccBankResultRequestDto : PagedResultRequestDto
    {
        public long? SupplierAuto { get; set; }
    }
}
