using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Abp.Domain.Entities.Auditing;
using CarPlusGo.CVAS.Car;
using CarPlusGo.CVAS.Common;

namespace CarPlusGo.CVAS.TakeCarFile
{
    [Table("TakeCar")]
    public class TakeCar:FullAuditedEntity<long>
    {
        [Column("CarTakeID")]
        public override long Id { get; set; }
        public long CarTakeApplyID { get; set; }
        public long CarBaseID { get; set; }
        public long? Status { get; set; }//车辆提领状态
        public int? ItemStatus { get; set; }
        [ForeignKey("ItemStatus,Status")]
        public ItemCode ItemStatusCode { get; set; }
        [ForeignKey("CarTakeApplyID")]
        public TakeCarApply TakeCarApply { get; set; }
        [ForeignKey("CarBaseID")]
        public CarBase CarBase { get; set; }
    }
}
