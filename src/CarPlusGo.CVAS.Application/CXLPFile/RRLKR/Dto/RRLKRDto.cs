using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarPlusGo.CVAS.CXLPFile.Dto
{
    [AutoMap(typeof(RRLKR))]
    public class RRLKRDto : FullAuditedEntityDto<long>
    {
        public int FormType { get; set; }
        public string LkrUser { get; set; }
        public string LkrAccount { get; set; }
        public string LkrBank { get; set; }
        public int BankType { get; set; }
        public long Cuser { get; set; }
        public DateTime Cdt { get; set; }
    }
}
