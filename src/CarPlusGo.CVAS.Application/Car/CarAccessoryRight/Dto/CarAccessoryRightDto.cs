using System;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using CarPlusGo.CVAS.Common.Dto;

namespace CarPlusGo.CVAS.Car.Dto
{
    [AutoMap(typeof(CarAccessoryRight))]//适用随车配件能源别
    public class CarAccessoryRightDto:FullAuditedEntityDto<long>
    {
        public long CarAccessoryID { get; set; }//配件ID
        public long? OilID { get; set; }//燃油类型
        public int Selected { get; set; }//是否停用
        public int? OilType { get; set; }//231
        public ItemCodeDto ItemCode { get; set; }
        public CarAccessoryDto CarAccessory { get; set; }
    }
}
