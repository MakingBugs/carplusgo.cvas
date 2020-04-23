using Abp.Application.Services.Dto;

namespace CarPlusGo.CVAS.BPM.Dto
{
    public class PagedFileUploadResultRequestDto : PagedResultRequestDto
    {
        public long? DocPostID { get; set; }
    }
}
