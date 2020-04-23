using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarPlusGo.CVAS.CXLPFile.DTO
{
    public class CXLPResultRequestDto : PagedResultRequestDto
    {
        public int? CaseType { get; set; }
        public int? CaseStatus { get; set; }
        public string MakNo { get; set; }
        public string InsureNo { get; set; }
        public string CXLPNO { get; set; }
        public DateTime? CaseTime { get; set; }
        public DateTime? CaseTimeFrom { get; set; }
        public DateTime? CaseTimeTo { get; set; }
        public DateTime? ZhuanFuDTFrom { get; set; }
        public DateTime? ZhuanFuDTTo { get; set; }
    }
}
