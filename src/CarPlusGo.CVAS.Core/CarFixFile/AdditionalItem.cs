using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarPlusGo.CVAS.CarFixFile
{
    [Table("AdditionalItem")]
    public class AdditionalItem : FullAuditedEntity<long>
    {
        [Column("AdditionalItem_Auto")]
        public override long Id { get; set; }
        [Column("CarRepair_Auto")]
        public long CarRepairAuto { get; set; }
        [Column("AdditionalItem")]
        public string AdditionalItem1 { get; set; }
        public int Status { get; set; }
        public int SerialNumber { get; set; }
        public long Cuser { get; set; }
        public DateTime Cdt { get; set; }
        public long? Muser { get; set; }
        public DateTime? Mdt { get; set; }
    }
}
