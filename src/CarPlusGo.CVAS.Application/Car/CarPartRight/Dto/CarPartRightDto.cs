using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services.Dto;
using CarPlusGo.CVAS.Common.Dto;

namespace CarPlusGo.CVAS.Car.Dto
{
    [AutoMap(typeof(CarPartRight))]//适用车辆部位能源别
    public class CarPartRightDto:FullAuditedEntityDto<long>
    {
        public long CarPartID { get; set; }//车辆部位ID
        public long? OilID { get; set; }//燃油ID
        public int Selected { get; set; }//是否停用
        public int? OilType { get; set; }//231
        public ItemCodeDto ItemCode { get; set; }
        public CarPartDto CarPart { get; set; }

    }
}
