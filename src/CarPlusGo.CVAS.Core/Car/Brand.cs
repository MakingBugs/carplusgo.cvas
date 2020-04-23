using Abp.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarPlusGo.CVAS.Car
{
    /// <summary>
    /// 厂牌
    /// </summary>
    [Table("Brand")]
    public class Brand : Entity<long>
    {
        [Column("Brand_Auto")]
        public override long Id { get; set; }
        public string BrandName { get; set; }
        public DateTime Cdt { get; set; }
        public long Cuser { get; set; }
        public DateTime Mdt { get; set; }
        public long Muser { get; set; }
        [ForeignKey("FactoryBrand")]
        [Column("FactoryBrand_Auto")]
        public long FactoryBrandAuto { get; set; }
        public FactoryBrand FactoryBrand { get; set; }
    }
}
