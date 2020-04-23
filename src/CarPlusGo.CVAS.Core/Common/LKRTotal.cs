using Abp.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarPlusGo.CVAS.Common
{
    [Table("LKR_Total")]
    public class LKRTotal : Entity<long>
    {
        [Column("LKR_Total_Auto")]
        public override long Id { get; set; }
        public string LKRName { get; set; }
        public string LKRBank { get; set; }
        public int LKRBankType { get; set; }
        [ForeignKey("LKRBankType")]
        public BankType BankType { get; set; }
        public string LKRAcount { get; set; }
        public int IsOn { get; set; }
        public long CUser { get; set; }
        public DateTime CDT { get; set; }
        public int? MUser { get; set; }
        public DateTime? MDT { get; set; }
    }
}
