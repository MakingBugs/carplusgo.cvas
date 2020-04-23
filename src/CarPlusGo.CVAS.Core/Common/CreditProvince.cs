using Abp.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarPlusGo.CVAS.Common
{
    [Table("CreditProvince")]
    public class CreditProvince : Entity<long>
    {
        [Column("CreditProvince_Auto")]
        public override long Id { get; set; }
        [Column("code")]
        public long Code { get; set; }
        [Column("name")]
        public string Name { get; set; }
    }
}