using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CarPlusGo.CVAS.Car
{
    [Table("CarAccessory")]
    public class CarAccessory:FullAuditedEntity<long>
    {
        [Column("CarAccessoryID")]
        public override long Id { get; set; }
        public string CarAccessoryName { get; set; }
        public int Qty { get; set; }//数量
        public int IsStop { get; set; }
    }
}
