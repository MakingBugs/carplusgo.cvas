using Abp.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarPlusGo.CVAS.Accessories
{
    [Table("AccessoriesType")]
    public class AccessoriesType : Entity<long>
    {
        [Column("AccessoriesType_Auto")]
        public override long Id { get; set; }
        [ForeignKey("AccessoriesMainType")]
        [Column("AccessoriesMainType_Auto")]
        public long AccessoriesMainTypeAuto { get; set; }
        public AccessoriesMainType AccessoriesMainType { get; set; }
        public string AccessoriesTypeName { get; set; }
        public int? PurchasePrice { get; set; }
        public int? SellingPrice { get; set; }
        public long Cuser { get; set; }
        public DateTime Cdt { get; set; }
        public long? Muser { get; set; }
        public DateTime? Mdt { get; set; }
    }
}
