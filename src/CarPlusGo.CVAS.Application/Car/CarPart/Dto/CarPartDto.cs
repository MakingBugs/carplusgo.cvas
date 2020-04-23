using System;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace CarPlusGo.CVAS.Car.Dto
{
    [AutoMap(typeof(CarPart))]//车辆部位表
    public class CarPartDto: FullAuditedEntityDto<long>
    {
        public string CarPartName { get; set; }//部位名称
        public int IsStop { get; set; }//是否停用
    }
}

