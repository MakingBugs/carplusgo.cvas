using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services.Dto;
using CarPlusGo.CVAS.Car.Dto;
using CarPlusGo.CVAS.Common.Dto;

namespace CarPlusGo.CVAS.TakeCarFile.Dto
{
    [AutoMap(typeof(TakeCar))]//提领车辆
    public class TakeCarDto:FullAuditedEntityDto<long>
    {
        public long CarTakeApplyID { get; set; }
        public long CarBaseID { get; set; }
        public long? Status { get; set; }//车辆提领状态
        public int? ItemStatus { get; set; }
        public ItemCodeDto ItemStatusCode { get; set; }
        public TakeCarApplyDto TakeCarApply { get; set; }
        public CarBaseDto CarBase { get; set; }
    }
}
