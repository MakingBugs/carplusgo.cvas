using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarPlusGo.CVAS.Common.Dto
{
    public class PRInvLinkResultRequestDto : PagedResultRequestDto
    {
        public long? SourceAuto { get; set; }
    }
}
