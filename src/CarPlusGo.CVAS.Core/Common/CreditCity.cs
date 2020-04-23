using Abp.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarPlusGo.CVAS.Common
{
    [Table("CreditCity")]
    public class CreditCity : Entity<long>
    {
        [Column("CreditCity_Auto")]
        public override long Id { get; set; }
        [Column("code")]
        public long Code { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("provinceId")]
        public long ProvinceId { get; set; }
    }
}
