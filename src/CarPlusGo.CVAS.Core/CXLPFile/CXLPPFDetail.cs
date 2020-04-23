using Abp.Domain.Entities.Auditing;
using CarPlusGo.CVAS.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CarPlusGo.CVAS.CXLPFile
{
    [Table("CXLPPFDetail")]
    public class CXLPPFDetail : FullAuditedEntity<long>
    {
        [Column("CXLPPFDetail_Auto")]
        public override long Id { get; set; }
        [Column("CXLP_Auto")]
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
        [ForeignKey("BankType")]
        public BankType BankTypeData { get; set; }
    }
}
