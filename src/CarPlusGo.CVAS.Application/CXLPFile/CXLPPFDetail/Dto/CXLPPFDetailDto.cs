using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using CarPlusGo.CVAS.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarPlusGo.CVAS.CXLPFile.Dto
{
    [AutoMap(typeof(CXLPPFDetail))]
    public class CXLPPFDetailDto : FullAuditedEntityDto<long>
    {
        public long CxlpAuto { get; set; }
        public string AccountName { get; set; }
        public string AcountBank { get; set; }
        public string BankAccount { get; set; }
        public decimal Pfamt { get; set; }
        public long Cuser { get; set; }
        public DateTime Cdt { get; set; }
        public long? Muser { get; set; }
        public DateTime? Mdt { get; set; }
        public int? BankType { get; set; }
        public BankType BankTypeData { get; set; }
    }
}
