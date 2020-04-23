using Abp.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarPlusGo.CVAS.Common
{
    [Table("ItemCode")]
    public class ItemCode : Entity<long>
    {
        [Column("ItemCode_Auto")]
        public override long Id { get; set; }
        public int ItemType { get; set; }
        public long Num { get; set; }
        public string ItemName { get; set; }
        public string Memo { get; set; }
        public int IsActive { get; set; }
        public int Seq { get; set; }
        public decimal V1 { get; set; }
        public decimal V2 { get; set; }
        public decimal A1 { get; set; }
    }
}
