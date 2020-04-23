using Abp.Domain.Entities.Auditing;
using CarPlusGo.CVAS.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CarPlusGo.CVAS.Car
{
    public class CarAccessoryRight: FullAuditedEntity<long>
    {
        [Column("CarAccessoryRightID")]
        public override long Id { get; set; }
        public long CarAccessoryID { get; set; }//配件ID
        public long? OilID { get; set; }//燃油类型
        public int Selected { get; set; }//是否停用
        public int? OilType { get; set; }
        [ForeignKey("OilType,OilID")]
        public ItemCode ItemCode { get; set; }
        [ForeignKey("CarAccessoryID")]
        public CarAccessory CarAccessory { get; set; }
    }
}
