using Abp.Domain.Entities.Auditing;
using CarPlusGo.CVAS.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CarPlusGo.CVAS.CXLPFile
{
    [Table("CXLPSupplement")]
    public class CXLPSupplement : FullAuditedEntity<long>
    {
        [Column("CXLPSupplement_Auto")]
        public override long Id { get; set; }
        [Column("CXLP_Auto")]
        public long CXLPAuto { get; set; }
        public string SupplementContent { get; set; }
        public long Contractors { get; set; }
        public int CIC { get; set; }
        [ForeignKey("CIC,Contractors")]
        public ItemCode ContractorsItemCode { get; set; }
        public long CUser { get; set; }
        public DateTime CDT { get; set; }
        public long? MUser { get; set; }
        public DateTime? MDT { get; set; }
    }
}
