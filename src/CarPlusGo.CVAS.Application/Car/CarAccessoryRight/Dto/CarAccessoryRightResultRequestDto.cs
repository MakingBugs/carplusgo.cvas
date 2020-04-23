using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarPlusGo.CVAS.Car.Dto
{
    public class CarAccessoryRightResultRequestDto: PagedResultRequestDto
    {
        public long? CarAccessoryID { get; set; }//配件ID
        public int? Selected { get; set; }//是否停用
    }
}
