using Abp.Domain.Entities.Auditing;
using CarPlusGo.CVAS.Mobile.TShareBank.Enum;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarPlusGo.CVAS.Mobile.TShareBank
{
    [Table("TargetConfig")]
    public class TargetConfig : FullAuditedEntity<long>
    {
        public DateTime From { get;set; }
        public DateTime To { get; set; }
        public decimal OrderAmount { get; set; }
        public long OrderCount { get; set; }
        public long OnlineDriverNum { get; set; }
        public decimal DriverDailyOrderNum { get; set; }
        public long RegisterUserNum { get; set; }
        public long DailyActivityNum { get; set; }
        public Unit Unit { get; set; }
    }
}
