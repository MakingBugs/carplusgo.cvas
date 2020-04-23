using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services.Dto;
using CarPlusGo.CVAS.Car.Dto;
using CarPlusGo.CVAS.TakeCarFile.Dto;
using CarPlusGo.CVAS.LocationFile.Dto;
using CarPlusGo.CVAS.Common.Dto;

namespace CarPlusGo.CVAS.RepositoryOutCar.Dto
{
    [AutoMap(typeof(RepositoryOut))]//出入库记录档
    public class RepositoryOutDto:FullAuditedEntityDto<long>
    {
        public long CarTakeApplyID { get; set; }//车辆提领申请编号
        public long CarTakeID { get; set; }//车辆提领编号
        public long CarBaseAuto { get; set; }
        public string MakNo { get; set; }//车牌号
        public long OutRepositoryID { get; set; }//出库仓库
        public long OutReason { get; set; }//出库原因
        public DateTime OutDate { get; set; }//出库日期
        public int OutKM { get; set; }//出库里程
        public int OutCondition { get; set; }//出库车况
        public string OutMemo { get; set; }//出库备注
        public long InRepositoryID { get; set; }//入库仓库
        public long InReason { get; set; }//入库原因
        public DateTime InDate { get; set; }//入库日期
        public int InKM { get; set; }//入库里程
        public int InCondition { get; set; }//入库车况
        public string InMemo { get; set; }//入库备注
        public string RejectMemo { get; set; }//取消原因
        public long Status { get; set; }//出库状态1608
        public int ItemStatus { get; set; }
        public int ItemOutReason { get; set; }
        public int ItemInReason { get; set; }
        public CarBaseDto CarBase { get; set; }
        public TakeCarApplyDto TakeCarApply { get; set; }
        public TakeCarDto TakeCar { get; set; }
        public RepositoryDto OutRepositoryData { get; set; }
        public RepositoryDto InRepositoryData { get; set; }
        public ItemCodeDto ItemCodeStatus { get; set; }
        public ItemCodeDto ItemCodeOutReason { get; set; }
        public ItemCodeDto ItemCodeInReason { get; set; }
    }
}
