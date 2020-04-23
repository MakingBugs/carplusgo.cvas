using Abp.Application.Services.Dto;
using System;

namespace CarPlusGo.CVAS.Finance.Dto
{
    public class PagedAccountingEntryConfigResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }

        public bool? IsActive { get; set; }

        public bool? IsMaster { get; set; }

        public DateTime? From { get; set; }

        public DateTime? To { get; set; }
    }
}
