using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using CarPlusGo.CVAS.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarPlusGo.CVAS.CXLPFile.Dto
{
    [AutoMap(typeof(CXLPRecord))]
    public class CXLPRecordDto : FullAuditedEntityDto<long>
    {
        public long CxlpAuto { get; set; }
        public string RecordContent { get; set; }
        public long? Contractors { get; set; }
        public long? CaseDealWay { get; set; }
        public long? Cuser { get; set; }
        public DateTime? Mdt { get; set; }
        public DateTime Cdt { get; set; }
        public long? Muser { get; set; }
        public int CDWIC { get; set; }
        public int CIC { get; set; }
        public ItemCode CaseDealWayItemCode { get; set; }
        public ItemCode ContractorsItemCode { get; set; }
        public VEmp CVEmp { get; set; }
        public VEmp MVEmp { get; set; }
    }
}
