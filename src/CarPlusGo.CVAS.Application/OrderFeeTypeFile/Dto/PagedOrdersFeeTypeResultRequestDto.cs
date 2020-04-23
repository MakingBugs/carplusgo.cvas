using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarPlusGo.CVAS.OrdersFeeTypeFile.Dto
{
    public class PagedOrdersFeeTypeResultRequestDto: PagedResultRequestDto
    {
        public long Inc_Auto { get; set; }
        
    }
}
