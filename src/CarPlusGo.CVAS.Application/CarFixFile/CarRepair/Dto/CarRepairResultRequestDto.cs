using Abp.Application.Services.Dto;

namespace CarPlusGo.CVAS.CarFixFile.Dto
{
    public class CarRepairResultRequestDto : PagedResultRequestDto
    {
        public long? CarBaseAuto { get; set; }
        public int? Status { get; set; }
    }
}
