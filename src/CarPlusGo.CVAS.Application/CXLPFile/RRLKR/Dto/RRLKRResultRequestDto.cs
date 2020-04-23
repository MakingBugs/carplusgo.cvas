using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarPlusGo.CVAS.CXLPFile.Dto
{
    public class RRLKRResultRequestDto : PagedResultRequestDto
    {
        public long? RRLKRAuto { get; set; }
        public int? FormType { get; set; }
    }
}
