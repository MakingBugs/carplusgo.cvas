using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CarPlusGo.CVAS.CarFixFile
{
    [Table("CarFixItem")]
    public class CarFixItem : FullAuditedEntity<long>
    {
        [Column("CarFixItem_Auto")]
        public override long Id { get; set; }
        [Column("CarFix_Auto")]
        public long CarFixAuto { get; set; }
        public string ItemName { get; set; }
        public int ItemCount { get; set; }
        public decimal ItemAmount { get; set; }
        public int PayType { get; set; }
        public int ItemType { get; set; }
        public string Memo { get; set; }
        public int Seq { get; set; }
        public int CUser { get; set; }
        public DateTime CDT { get; set; }
        public int MUser { get; set; }
        public DateTime MDT { get; set; }
    }
}
