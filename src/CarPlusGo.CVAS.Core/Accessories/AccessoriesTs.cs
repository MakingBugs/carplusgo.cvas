using Abp.Domain.Entities;
using CarPlusGo.CVAS.Common;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarPlusGo.CVAS.Accessories
{
    [Table("Accessories_TS")]
    public class AccessoriesTs : Entity<long>
    {
        [Column("Accessories_TS_Auto")]
        public override long Id { get; set; }
        [Column("AccessoriesType_Auto")]
        [ForeignKey("AccessoriesType")]
        public long AccessoriesTypeAuto { get; set; }
        public AccessoriesType AccessoriesType { get; set; }
        public int ItemType { get; set; }
        [Column("Supplier")]
        public long Supplier { get; set; }
        [ForeignKey("ItemType,Supplier")]
        public ItemCode ItemCode { get; set; }
        public int PurchasePrice { get; set; }
        public int SellingPrice { get; set; }
        public long Cuser { get; set; }
        public DateTime Cdt { get; set; }
        public long? Muser { get; set; }
        public DateTime? Mdt { get; set; }
        public int? CostPrice { get; set; }
    }
}
