using System.Linq;

namespace CarPlusGo.CVAS.Mobile.Analysis.Dto
{
    public class OrderInfoTrendDto
    {
        public IOrderedEnumerable<OrderInfoDetailDto> TotalOrder { get; set; }
        public IOrderedEnumerable<OrderInfoDetailDto> BookingOrder { get; set; }
        public IOrderedEnumerable<OrderInfoDetailDto> ImmediateOrder { get; set; }
        public IOrderedEnumerable<OrderInfoDetailDto> PickupOrder { get; set; }
        public IOrderedEnumerable<OrderInfoDetailDto> DropoffOrder { get; set; }
    }
}
