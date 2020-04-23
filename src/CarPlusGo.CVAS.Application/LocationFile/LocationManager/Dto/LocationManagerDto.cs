using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarPlusGo.CVAS.LocationFile.Dto
{
    [AutoMap(typeof(LocationManager))]//区域管理员表
    public class LocationManagerDto : FullAuditedEntityDto<long>
    {
        public long AreaID { get; set; }//区域ID
        public int RepositoryType { get; set; }//仓库类型
        public string EmpID { get; set; }//员工编号
        public int IsStop { get; set; }//是否停用
        public Location Location { get; set; }
    }
}
