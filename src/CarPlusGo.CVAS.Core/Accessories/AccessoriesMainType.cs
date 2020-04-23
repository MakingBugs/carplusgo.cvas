using Abp.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarPlusGo.CVAS.Accessories
{
    [Table("AccessoriesMainType")]
    public class AccessoriesMainType : Entity<long>
    {
        [Column("AccessoriesMainType_Auto")]
        public override long Id { get; set; }
        public string AccessoriesMainName { get; set; }
        public long Cuser { get; set; }
        public DateTime Cdt { get; set; }
        public long? Muser { get; set; }
        public DateTime? Mdt { get; set; }
    }
}
