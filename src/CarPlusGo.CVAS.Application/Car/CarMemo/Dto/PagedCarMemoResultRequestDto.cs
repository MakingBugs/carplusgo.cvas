using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarPlusGo.CVAS.Car.Dto
{
    public class PagedCarMemoResultRequestDto : PagedResultRequestDto
    {
        public string CarMakNo { get; set; }
    }
}
