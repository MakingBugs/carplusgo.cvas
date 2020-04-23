using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using CarPlusGo.CVAS.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarPlusGo.CVAS.CXLPFile.Dto
{
    [AutoMap(typeof(CXLPFee))]
    public class CXLPFeeDto : FullAuditedEntityDto<long>
    {
        public long CxlpAuto { get; set; }
        public long Dzftype { get; set; }
        public int? DTIC { get; set; }
        public ItemCode DZFTypeItemCode { get; set; }
        public long FeeType { get; set; }
        public int? FTIC { get; set; }
        public ItemCode FeeTypeItemCode { get; set; }
        public decimal FeeAmount { get; set; }
        public string FeeOtherRemark { get; set; }
        public long Cuser { get; set; }
        public DateTime Cdt { get; set; }
        public long Muser { get; set; }
        public DateTime? Mdt { get; set; }
    }
}
