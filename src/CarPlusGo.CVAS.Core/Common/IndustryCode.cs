using Abp.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarPlusGo.CVAS.Common
{
    [Table("IndustryCode")]
    public class IndustryCode : Entity<long>
    {
        [Column("IndustryCode_Auto")]
        public override long Id { get; set; }
        public string Industrycode { get; set; }
        public string IndustryName { get; set; }
        public string Memo { get; set; }
    }
}
