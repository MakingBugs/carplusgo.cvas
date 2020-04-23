using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarPlusGo.CVAS.LocationFile.Dto
{
    [AutoMap(typeof(RepositoryManager))]//仓库管理员表
    public class RepositoryManagerDto : FullAuditedEntityDto<long>
    {
        public long RepositoryID { get; set; }//仓库编号
        public int ManagerID { get; set; }//员工编号
        public int IsStop { get; set; }//是否停用
        public int IsManager { get; set; }//是否是库长
        public RepositoryDto Repository { get; set; }
    }
}
