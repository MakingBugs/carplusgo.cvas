using System.Linq;

namespace CarPlusGo.CVAS.Mobile.Analysis.Dto
{
    public class PerformanceTrendDto
    {
        public IOrderedEnumerable<PerformanceDetailDto> TotalOrder { get; set; }
        public IOrderedEnumerable<PerformanceDetailDto> BookingOrder { get; set; }
        public IOrderedEnumerable<PerformanceDetailDto> ImmediateOrder { get; set; }
        public IOrderedEnumerable<PerformanceDetailDto> PickupOrder { get; set; }
        public IOrderedEnumerable<PerformanceDetailDto> DropoffOrder { get; set; }
    }
}
