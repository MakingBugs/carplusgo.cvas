using Abp.Application.Services.Dto;
using System;

namespace CarPlusGo.CVAS.Mobile.Dto
{
    public class PagedTargetConfigResultRequestDto : PagedResultRequestDto
    {
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
    }
}
