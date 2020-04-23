using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services.Dto;
using CarPlusGo.CVAS.Common.Dto;
using CarPlusGo.CVAS.Car.Dto;

namespace CarPlusGo.CVAS.RepositoryOutCar.Dto
{
    [AutoMap(typeof(RepositoryOutCarPart))]//出入库车辆部位记录
    public class RepositoryOutCarPartDto:FullAuditedEntityDto<long>
    {
        public long RepositoryOutID { get; set; }//出入库档ID
        public long Type { get; set; }//出入库记录 1出库 2入库
        public long CarPartID { get; set; }//车辆部位
        public long? Status { get; set; }//车辆外观 1602
        public string Memo { get; set; }//备注
        public int? ItemStatus { get; set; }
        public RepositoryOutDto RepositoryOut { get; set; }
        public CarPartDto CarPart { get; set; }
        public ItemCodeDto ItemCode { get; set; }
    }
}
