using Abp.Domain.Entities;
using CarPlusGo.CVAS.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarPlusGo.CVAS.OrdersFeeTypeFile
{
    [Table("OrdersFeeType")]
    public class OrdersFeeType : Entity<long>
    { 
        [Column("OrdersFeeType_Auto")]
        public override long Id { get; set; }
        [ForeignKey("Inc")]
        [Column("Inc_Auto")]
        public long IncAuto { get; set; }
        public Inc Inc { get; set; } 
        [Column("FeeType_Auto")]
        public int FeeTypeAuto { get; set; } 
        public decimal FeeAmt { get; set; }
        public long Cuser { get; set; }
        public DateTime Cdt { get; set; }
        public long? Muser { get; set; }
        public DateTime? Mdt { get; set; }
        public int? IsLock { get; set; }  
    }
}
