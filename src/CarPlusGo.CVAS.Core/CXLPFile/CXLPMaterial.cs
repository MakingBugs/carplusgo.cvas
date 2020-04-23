using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CarPlusGo.CVAS.CXLPFile
{
    [Table("CXLPMaterial")]
    public class CXLPMaterial : FullAuditedEntity<long>
    {
        [Column("CXLPMaterial_Auto")]
        public override long Id { get; set; }
        [Column("CXLP_Auto")]
        public long CXLPAuto { get; set; }
        public string CXLPMaterialName { get; set; }
        public string CXLPMaterialURL { get; set; }
        public int CXLPMaterialType { get; set; }
        public long CUser { get; set; }
        public DateTime CDT { get; set; }
        public long MUser { get; set; }
        public DateTime? MDT { get; set; }
        public string FileSize { get; set; }
        public string OldFileName { get; set; }
    }
}
