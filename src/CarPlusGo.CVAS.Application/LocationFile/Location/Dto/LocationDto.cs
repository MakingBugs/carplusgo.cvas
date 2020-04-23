using System;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace CarPlusGo.CVAS.LocationFile.Dto
{
    [AutoMap(typeof(Location))]//区域表
    public class LocationDto:FullAuditedEntityDto<long>
    {
        public string AreaName { get; set; }//区域名称
        public int IsStop { get; set; }//是否停用
    }
}
