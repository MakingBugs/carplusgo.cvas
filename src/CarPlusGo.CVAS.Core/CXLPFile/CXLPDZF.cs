using Abp.Domain.Entities.Auditing;
using CarPlusGo.CVAS.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CarPlusGo.CVAS.CXLPFile
{
    [Table("CXLPDZF")]
    public class CXLPDZF  : FullAuditedEntity<long>
    {
        [Column("CXLPDZF_Auto")]
        public override long Id { get; set; }
        [Column("CXLP_Auto")]
        public long CxlpAuto { get; set; }
        public long Dzftype { get; set; }
        public int? Dtic { get; set; }
        [ForeignKey("Dtic,Dzftype")]
        public ItemCode DZFTypeItemCode { get; set; }
        public int IsInjured { get; set; }
        public string MakNo { get; set; }
        public string ClasenName { get; set; }
        public string Insinc { get; set; }
        public string Owner { get; set; }
        public string Driver { get; set; }
        public string Phone { get; set; }
        public string Remark { get; set; }
        public long NeedFile { get; set; }
        public long MakeFile { get; set; }
        public int NeedFileNum { get; set; }
        public int MakeFileNum { get; set; }
        public long Cuser { get; set; }
        public DateTime Cdt { get; set; }
        public long Muser { get; set; }
        public DateTime? Mdt { get; set; }
        public long? Tjclfile { get; set; }
        public long? Tjclnum { get; set; }
        public long? Qjdbfile { get; set; }
        public long? Qjdbnum { get; set; }
        public string Dzfwxc { get; set; }
    }
}
