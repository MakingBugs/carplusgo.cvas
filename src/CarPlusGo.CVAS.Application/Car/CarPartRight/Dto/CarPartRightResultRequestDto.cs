using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarPlusGo.CVAS.Car.Dto
{
    public class CarPartRightResultRequestDto : PagedResultRequestDto
    {
        public long? CarPartID { get; set; }//车辆部位ID
        public int? Selected { get; set; }//是否停用
        public long[] OilIds { get; set; }//燃油ID
    }
}
