using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using CarPlusGo.CVAS.Car.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarPlusGo.CVAS.TakeCarFile.Dto
{
    [AutoMap(typeof(TakeCar))]//提领车辆
    public class CreateTakeCarDto: FullAuditedEntityDto<long>
    {
        public long CarTakeApplyID { get; set; }
        public long CarBaseID { get; set; }
        public long? Status { get; set; }//车辆提领状态
        public int? ItemStatus { get; set; }

    }
}
