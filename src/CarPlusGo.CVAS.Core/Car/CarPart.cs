using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Abp.Domain.Entities.Auditing;

namespace CarPlusGo.CVAS.Car
{
    [Table("CarPart")]
    public class CarPart:FullAuditedEntity<long>
    {
        [Column("CarPartID")]
        public override long Id { get; set; }
        public string CarPartName { get; set; }//部位名称
        public int IsStop { get; set; }
    }
}
