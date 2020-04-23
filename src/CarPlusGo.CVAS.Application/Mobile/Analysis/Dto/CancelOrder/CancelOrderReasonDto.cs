using System.Linq;

namespace CarPlusGo.CVAS.Mobile.Analysis.Dto
{
    public class CancelOrderReasonDto
    {
        public IOrderedEnumerable<CancelOrderReasonDetailDto> TotalOrder { get; set; }
        public IOrderedEnumerable<CancelOrderReasonDetailDto> BookingOrder { get; set; }
        public IOrderedEnumerable<CancelOrderReasonDetailDto> ImmediateOrder { get; set; }
        public IOrderedEnumerable<CancelOrderReasonDetailDto> PickupOrder { get; set; }
        public IOrderedEnumerable<CancelOrderReasonDetailDto> DropoffOrder { get; set; }
    }
}
