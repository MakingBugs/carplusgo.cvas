using System.Linq;

namespace CarPlusGo.CVAS.Mobile.Analysis.Dto
{
    public class OrderTrendDto
    {
        public IOrderedEnumerable<OrderDetailDto> TotalOrder { get; set; }
        public IOrderedEnumerable<OrderDetailDto> BookingOrder { get; set; }
        public IOrderedEnumerable<OrderDetailDto> ImmediateOrder { get; set; }
        public IOrderedEnumerable<OrderDetailDto> PickupOrder { get; set; }
        public IOrderedEnumerable<OrderDetailDto> DropoffOrder { get; set; }
    }
}
