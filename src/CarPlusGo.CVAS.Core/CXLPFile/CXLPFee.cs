using Abp.Domain.Entities.Auditing;
using CarPlusGo.CVAS.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CarPlusGo.CVAS.CXLPFile
{
    [Table("CXLPFee")]
    public class CXLPFee : FullAuditedEntity<long>
    {
        [Column("CXLPFee_Auto")]
        public override long Id { get; set; }
        [Column("CXLP_Auto")]
        public long CxlpAuto { get; set; }
        public long Dzftype { get; set; }
        public int? DTIC { get; set; }
        [ForeignKey("DTIC,Dzftype")]
        public ItemCode DZFTypeItemCode { get; set; }
        public long FeeType { get; set; }
        public int? FTIC { get; set; }
        [ForeignKey("FTIC,FeeType")]
        public ItemCode FeeTypeItemCode { get; set; }
        public decimal FeeAmount { get; set; }
        public string FeeOtherRemark { get; set; }
        public long Cuser { get; set; }
        public DateTime Cdt { get; set; }
        public long Muser { get; set; }
        public DateTime? Mdt { get; set; }
    }
}
