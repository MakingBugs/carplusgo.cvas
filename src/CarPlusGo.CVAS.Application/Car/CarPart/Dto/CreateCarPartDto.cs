using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarPlusGo.CVAS.Car.Dto
{
    [AutoMap(typeof(CarPart))]
    public class CreateCarPartDto : FullAuditedEntityDto<long>
    {
        public string CarPartName { get; set; }//部位名称
        public int IsStop { get; set; }//是否停用
        public List<CarPartRightDto> CarPartRightList { get; set; }
    }
}
