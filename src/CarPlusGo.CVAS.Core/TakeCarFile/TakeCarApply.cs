using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Abp.Domain.Entities.Auditing;
using CarPlusGo.CVAS.Car;
using CarPlusGo.CVAS.Common;
using CarPlusGo.CVAS.LocationFile;

namespace CarPlusGo.CVAS.TakeCarFile
{
    [Table("TakeCarApply")]
    public class TakeCarApply : FullAuditedEntity<long>
    {
        [Column("TakeCarApplyID")]
        public override long Id { get; set; }
        public long? Type { get; set; }//领用类型
        public long AreaID { get; set; }//提领区域
        public DateTime TakeDate { get; set; }//提领日期
        public int TakeQty { get; set; }//提领数量
        public long? Factory { get; set; }//总厂牌
        public long? Brand { get; set; }//厂牌
        public long? Clasen { get; set; }//车辆
        public string Memo { get; set; }//备注
        public string CancelMemo { get; set; }//取消备注
        public long? Status { get; set; }//提领申请状态
        public int? ItemType { get; set; }
        public int? ItemStatus { get; set; }
        [ForeignKey("AreaID")]
        public Location Location { get; set; }
        [ForeignKey("Factory")]
        public FactoryBrand FactoryBrand { get; set; }
        [ForeignKey("Brand")]
        public Brand BrandData { get; set; }
        [ForeignKey("Clasen")]
        public Clasen ClasenData { get; set; }
        [ForeignKey("ItemType,Type")]
        public ItemCode ItemTypeCode { get; set; }
        [ForeignKey("ItemStatus,Status")]
        public ItemCode ItemStatusCode { get; set; }
    }
}
