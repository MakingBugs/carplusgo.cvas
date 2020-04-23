using Abp.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarPlusGo.CVAS.Mobile.BiStat
{
    [Table("t_stat_driver_time_day")]
    public class DayDriverTime : Entity<long>
    {
        [Column("stat_id")]
        public override long Id { get; set; }
        [Column("driver_id")]
        public string DriverId { get; set; }
        [Column("online_times")]
        public double OnlineTimes { get; set; }
        [Column("order_times")]
        public double OrderTimes { get; set; }
        [Column("peak_times")]
        public double PeakTimes { get; set; }
        [Column("date")]
        public string Date { get; set; }
    }
}
