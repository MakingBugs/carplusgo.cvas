using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarPlusGo.CVAS.LocationFile.Dto
{
    public class LocationResultRequestDto: PagedResultRequestDto
    {
        public string AreaName { get; set; }
        public int? IsStop { get; set; }
    }
}
