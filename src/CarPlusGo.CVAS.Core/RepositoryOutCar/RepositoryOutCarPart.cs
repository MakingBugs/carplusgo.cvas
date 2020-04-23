using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Abp.Domain.Entities.Auditing;
using CarPlusGo.CVAS.Car;
using CarPlusGo.CVAS.Common;

namespace CarPlusGo.CVAS.RepositoryOutCar
{
    [Table("RepositoryOutCarPart")]//出入库车辆部位记录
    public class RepositoryOutCarPart:FullAuditedEntity<long>
    {
        [Column("RepositoryOutCarPartID")]
        public override long Id { get; set; }
        public long RepositoryOutID { get; set; }
        public long Type { get; set; }//出入库记录 1出库 2入库
        public long CarPartID { get; set; }//车辆部位
        public long? Status { get; set; }//车辆外观 1602
        public string Memo { get; set; }//备注
        public int? ItemStatus { get; set; }
        [ForeignKey("RepositoryOutID")]
        public RepositoryOut RepositoryOut { get; set; }
        [ForeignKey("CarPartID")]
        public CarPart CarPart { get; set; }
        [ForeignKey("ItemStatus,Status")]
        public ItemCode ItemCode { get; set; }
    }
}
