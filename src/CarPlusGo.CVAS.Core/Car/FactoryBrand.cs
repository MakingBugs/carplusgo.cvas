using Abp.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarPlusGo.CVAS.Car
{
    /// <summary>
    /// 总厂牌
    /// </summary>
    [Table("FactoryBrand")]
    public class FactoryBrand : Entity<long>
    {
        [Column("FactoryBrand_Auto")]
        public override long Id { get; set; }
        public string FactoryBrandName { get; set; }
        public DateTime Cdt { get; set; }
        public long Cuser { get; set; }
        public DateTime Mdt { get; set; }
        public long Muser { get; set; }
    }
}
