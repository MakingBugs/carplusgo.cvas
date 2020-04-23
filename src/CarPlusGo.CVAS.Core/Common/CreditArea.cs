using Abp.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarPlusGo.CVAS.Common
{
    [Table("CreditArea")]
    public class CreditArea : Entity<long>
    {
        [Column("CreditArea_Auto")]
        public override long Id { get; set; }
        [Column("code")]
        public long Code { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("cityId")]
        public long CityId { get; set; }
    }
}
