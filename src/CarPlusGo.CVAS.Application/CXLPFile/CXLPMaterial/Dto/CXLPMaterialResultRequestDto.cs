using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarPlusGo.CVAS.CXLPFile.Dto
{
    public class CXLPMaterialResultRequestDto : PagedResultRequestDto
    {
        public long? CXLPAuto { get; set; }
        public int? CXLPMaterialType { get; set; }
    }
}
