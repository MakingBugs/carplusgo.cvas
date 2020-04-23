using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarPlusGo.CVAS.Insure.Dto
{
    public class Insure2ResultRequestDto : PagedResultRequestDto
    {
        public long? CarBaseAuto { get; set; }
        public int? Year { get; set; }
        public int? InsureType { get; set; }
    }
}
