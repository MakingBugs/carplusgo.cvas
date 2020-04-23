using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarPlusGo.CVAS.OrdersFile.Dto
{
    public class PagedOrdersListResultRequestDto : PagedResultRequestDto
    {
        public long Orders_Auto { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }  
        public int? Status { get; set; }
    }
}
