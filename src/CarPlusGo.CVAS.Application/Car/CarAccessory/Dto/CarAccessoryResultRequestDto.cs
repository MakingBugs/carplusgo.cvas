using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarPlusGo.CVAS.Car.Dto
{
    public class CarAccessoryResultRequestDto: PagedResultRequestDto
    {
        public string CarAccessoryName { get; set; }//配件名称
        public int? IsStop { get; set; }
    }
}
