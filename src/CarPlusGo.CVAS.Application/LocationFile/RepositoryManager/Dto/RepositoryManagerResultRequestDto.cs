using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarPlusGo.CVAS.LocationFile.Dto
{
    public class RepositoryManagerResultRequestDto : PagedResultRequestDto
    {
        public int? IsStop { get; set; }//是否停用 0停用 1正常
        public int? ManagerID { get; set; }//员工编号
        public long? RepositoryID { get; set; }//仓库编号
    }
}
