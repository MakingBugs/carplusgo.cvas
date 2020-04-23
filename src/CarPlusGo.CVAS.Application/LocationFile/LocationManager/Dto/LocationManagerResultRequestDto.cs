using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarPlusGo.CVAS.LocationFile.Dto
{
    public class LocationManagerResultRequestDto : PagedResultRequestDto
    {
        public long? AreaID { get; set; }//区域ID
        public int? IsStop { get; set; }//是否停用
    }
}
