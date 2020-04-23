using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarPlusGo.CVAS.Car.Dto
{
    [AutoMap(typeof(CarAccessory))]//随车配件表
    public class CarAccessoryDto: FullAuditedEntityDto<long>
    {
        public string CarAccessoryName { get; set; }//配件名称
        public int Qty { get; set; }//数量
        public int IsStop { get; set; }//是否停用
    }
}
