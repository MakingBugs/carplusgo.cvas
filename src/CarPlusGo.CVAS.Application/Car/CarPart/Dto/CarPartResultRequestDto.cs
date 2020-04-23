using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarPlusGo.CVAS.Car.Dto
{
    public class CarPartResultRequestDto : PagedResultRequestDto
    {
        public string CarPartName { get; set; }//部位名称
        public int? IsStop { get; set; }
    }
}
