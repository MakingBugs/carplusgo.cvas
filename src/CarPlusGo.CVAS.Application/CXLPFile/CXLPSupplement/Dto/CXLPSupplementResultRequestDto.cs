using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarPlusGo.CVAS.CXLPFile.Dto
{
    public class CXLPSupplementResultRequestDto : PagedResultRequestDto
    {
        public long? CXLPAuto { get; set; }
    }
}
