using Abp.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarPlusGo.CVAS.Common
{
    [Table("BankDetail")]
    public class BankDetail : Entity<long>
    {
        [Column("BankDetail_Auto")]
        public override long Id { get; set; }
        public int? BankType { get; set; }
        [ForeignKey("BankType")]
        public BankType BankTypeData { get; set; }
        public string AreaNumber { get; set; }
        public string BankName { get; set; }
        public string BankNumber { get; set; }
        public int? InVisible { get; set; }
        public long Cuser { get; set; }
        public DateTime Cdt { get; set; }
        public long? Muser { get; set; }
        public DateTime? Mdt { get; set; }
    }
}
