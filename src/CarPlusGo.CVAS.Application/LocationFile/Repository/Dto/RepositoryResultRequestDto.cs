using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarPlusGo.CVAS.LocationFile.Dto
{
    public class RepositoryResultRequestDto : PagedResultRequestDto
    {
        public int? RepositoryType { get; set; }//仓库类型
        public string RepositoryName { get; set; }//仓库名称
        public long? AreaID { get; set; }//区域
        public int? IsStop { get; set; }//是否停用 0停用 1正常
    }
}
