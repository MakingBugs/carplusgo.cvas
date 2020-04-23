using Abp.Application.Services.Dto;
using CarPlusGo.CVAS.Mobile.TShareBank.Enum;
using System;

namespace CarPlusGo.CVAS.Mobile.Dto
{
    public class PagedMobileOrderResultRequestDto : PagedResultRequestDto
    {
        public DateTime? StartTimeFrom { get; set; }
        public DateTime? StartTimeTo { get; set; }
        public DateTime? EndTimeFrom { get; set; }
        public DateTime? EndTimeTo { get; set; }
        public string OrderNum { get; set; }
        public string PhoneNum { get; set; }
        public string CarNum { get; set; }
        public OrderStatus? OrderStatus { get; set; }
    }
}
