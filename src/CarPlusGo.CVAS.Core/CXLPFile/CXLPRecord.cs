using Abp.Domain.Entities.Auditing;
using CarPlusGo.CVAS.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CarPlusGo.CVAS.CXLPFile
{
    [Table("CXLPRecord")]
    public class CXLPRecord : FullAuditedEntity<long>
    {
        [Column("CXLPRecord_Auto")]
        public override long Id { get; set; }
        [Column("CXLP_Auto")]
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
        [ForeignKey("CDWIC,CaseDealWay")]
        public ItemCode CaseDealWayItemCode { get; set; }
        [ForeignKey("CIC,Contractors")]
        public ItemCode ContractorsItemCode { get; set; }
        [ForeignKey("Cuser")]
        public VEmp CVEmp { get; set; }
        [ForeignKey("Muser")]
        public VEmp MVEmp { get; set; }
    }
}
