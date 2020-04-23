namespace CarPlusGo.CVAS.Mobile.Analysis.Dto
{
    public class OrderDetailDto
    {
        public int Year { get; set; }
        public string Key { get; set; }
        public int OrderCount { get; set; }

        public int AcceptedOrderCount { get; set; }

        public int CompletedOrderCount { get; set; }

        public int SystemCancelOrderCount { get; set; }

        public int UnacceptedPassengerCancelOrderCount { get; set; }

        public int AcceptedPassengerCancelOrderCount { get; set; }
    }
}
