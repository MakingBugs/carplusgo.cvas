using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Abp.Domain.Entities.Auditing;
using CarPlusGo.CVAS.Car;
using CarPlusGo.CVAS.Common;
using CarPlusGo.CVAS.LocationFile;
using CarPlusGo.CVAS.TakeCarFile;

namespace CarPlusGo.CVAS.RepositoryOutCar
{
    [Table("RepositoryOut")]//出入库记录档
    public class RepositoryOut:FullAuditedEntity<long>
    {
        [Column("RepositoryOutID")]
        public override long Id { get; set; }
        public long CarTakeApplyID { get; set; }//车辆提领申请编号
        public long CarTakeID { get; set; }//车辆提领编号
        [Column("CarBase_Auto")]
        public long CarBaseAuto { get; set; }
        public string MakNo { get; set; }//车牌号
        public long OutRepositoryID { get; set; }//出库仓库
        public long OutReason { get; set; }//出库原因
        public int ItemOutReason { get; set; }
        public DateTime OutDate { get; set; }//出库日期
        public int OutKM { get; set; }//出库里程
        public int OutCondition { get; set; }//出库车况
        public string OutMemo { get; set; }//出库备注
        public long InRepositoryID { get; set; }//入库仓库
        public long InReason { get; set; }//入库原因
        public int ItemInReason { get; set; }
        public DateTime InDate { get; set; }//入库日期
        public int InKM { get; set; }//入库里程
        public int InCondition { get; set; }//入库车况
        public string InMemo { get; set; }//入库备注
        public string RejectMemo { get; set; }//取消原因
        public long Status { get; set; }//出库状态1608
        public int ItemStatus { get; set; }
        [ForeignKey("CarBaseAuto")]
        public CarBase CarBase { get; set; }
        [ForeignKey("CarTakeApplyID")]
        public TakeCarApply TakeCarApply { get; set; }
        [ForeignKey("CarTakeID")]
        public TakeCar TakeCar { get; set; }
        [ForeignKey("OutRepositoryID")]
        public Repository OutRepositoryData { get; set; }
        [ForeignKey("InRepositoryID")]
        public Repository InRepositoryData { get; set; }
        [ForeignKey("ItemStatus,Status")]
        public ItemCode ItemCodeStatus { get; set; }
        [ForeignKey("ItemOutReason,OutReason")]
        public ItemCode ItemCodeOutReason { get; set; }
        [ForeignKey("ItemInReason,InReason")]
        public ItemCode ItemCodeInReason { get; set; }
    }
}
