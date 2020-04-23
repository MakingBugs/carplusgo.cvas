using Abp.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarPlusGo.CVAS.Common
{
    [Table("Area")]
    public class Area : Entity<long>
    {
        [Column("Area_Auto")]
        public override long Id { get; set; }
        public string AreaTitle { get; set; }
        public string AreaMemo { get; set; }
        public int CUser { get; set; }
        public DateTime CDT { get; set; }
        public int MUser { get; set; }
        public DateTime MDT { get; set; }
    }
}
