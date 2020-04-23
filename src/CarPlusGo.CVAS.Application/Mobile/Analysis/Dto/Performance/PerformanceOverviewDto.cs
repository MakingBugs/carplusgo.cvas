using System;

namespace CarPlusGo.CVAS.Mobile.Analysis.Dto
{
    public class PerformanceOverviewDto
    {
        public PerformanceDetailDto BookingOrder { get; set; }
        public PerformanceDetailDto ImmediateOrder { get; set; }
        public PerformanceDetailDto LastPeriodBookingOrder { get; set; }
        public PerformanceDetailDto LastPeriodImmediateOrder { get; set; }
        public PerformanceDetailDto LastMonthSamePeriodBookingOrder { get; set; }
        public PerformanceDetailDto LastMonthSamePeriodImmediateOrder { get; set; }
        public PerformanceDetailDto PickupOrder { get; set; }
        public PerformanceDetailDto DropoffOrder { get; set; }
        public PerformanceDetailDto LastPeriodPickupOrder { get; set; }
        public PerformanceDetailDto LastPeriodDropoffOrder { get; set; }
        public PerformanceDetailDto LastMonthSamePeriodPickupOrder { get; set; }
        public PerformanceDetailDto LastMonthSamePeriodDropoffOrder { get; set; }
    }
}
