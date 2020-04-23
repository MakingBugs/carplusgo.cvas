namespace CarPlusGo.CVAS.Mobile.Analysis.Dto
{
    public class OrderInfoDto
    {
        public OrderInfoDetailDto TotalOrder { get; set; }
        public OrderInfoDetailDto BookingOrder { get; set; }
        public OrderInfoDetailDto ImmediateOrder { get; set; }
        public OrderInfoDetailDto PickupOrder { get; set; }
        public OrderInfoDetailDto DropoffOrder { get; set; }
    }
}
