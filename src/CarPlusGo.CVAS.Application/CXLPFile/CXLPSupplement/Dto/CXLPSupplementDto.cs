using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using CarPlusGo.CVAS.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarPlusGo.CVAS.CXLPFile.Dto
{
    [AutoMap(typeof(CXLPSupplement))]
    public class CXLPSupplementDto : FullAuditedEntityDto<long>
    {
        public long CXLPAuto { get; set; }
        public string SupplementContent { get; set; }
        public long Contractors { get; set; }
        public int CIC { get; set; }
        public ItemCode ContractorsItemCode { get; set; }
        public long CUser { get; set; }
        public DateTime CDT { get; set; }
        public long? MUser { get; set; }
        public DateTime? MDT { get; set; }
    }
}
