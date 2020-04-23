using Abp.Application.Services.Dto;
using System;

namespace CarPlusGo.CVAS.Mobile.Dto
{
    public class PagedOperationTargetResultRequestDto : PagedResultRequestDto, ISortedResultRequest
    {
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public string Sorting { get; set; }
    }
}
