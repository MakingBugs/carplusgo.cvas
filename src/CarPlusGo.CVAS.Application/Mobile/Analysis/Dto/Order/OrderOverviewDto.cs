namespace CarPlusGo.CVAS.Mobile.Analysis.Dto
{
    public class OrderOverviewDto
    {
        public OrderDetailDto TotalOrder { get; set; }
        public OrderDetailDto BookingOrder { get; set; }
        public OrderDetailDto ImmediateOrder { get; set; }
        public OrderDetailDto PickupOrder { get; set; }
        public OrderDetailDto DropoffOrder { get; set; }
    }
}
