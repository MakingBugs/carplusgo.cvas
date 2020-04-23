using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CarPlusGo.CVAS.Common
{
    [Table("Unit")]
    public class Unit:Entity<long>
    {
        [Column("Unit_Auto")]
        public override long Id { get; set; }
        public string UnitId { get; set; }
        public string UnitName { get; set; }
        public string UpUnit { get; set; }
        public string LevelId { get; set; }
        public int? LevelNo { get; set; }
        public string HRLevelId { get; set; }
        public string UpUnitId { get; set; }
        public int? IsOn { get; set; }
        public int? Cuser { get; set; }
        public DateTime? CDT { get; set; }
        public int? Muser { get; set; }
        public DateTime? MDT { get; set; }
        [Column("Inc_Auto")]
        public long? IncAuto { get; set; }
        [ForeignKey("IncAuto")]
        public Inc Inc { get; set; }
    }
}
