using CarPlusGo.CVAS.Mobile.TShareBank.Enum;
using System;

namespace CarPlusGo.CVAS.Mobile.Dto
{
    public class MobileOrderDto
    {
        public string OrderNum { get; set; }
        public string OrderType { get; set; }
        public string PlaceOrderTime { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string StartPoint { get; set; }
        public string StartPoinitLocation { get; set; }
        public string EndPoint { get; set; }
        public string EndPointLocation { get; set; }
        public string StartAddressReal { get; set; }
        public string StartPoinitLocationReal { get; set; }
        public string TravelEndAddress { get; set; }
        public string EndPointLocationReal { get; set; }
        public string OrderStatus { get; set; }
        //tab_invoice
        public string InvoiceStatus { get; set; }
        //tab_car (carAuthid)
        public string CarNum { get; set; }
        //tshare_user username
        public string DriverUserName { get; set; }
        //tshare_user username
        public string PassengerUserName { get; set; }
        /// <summary>
        /// tshare_pay,coupon_account,coupon_templates
        /// </summary>
        public string GiftType { get; set; }
        public int DriverEvaluation { get; set; }
        public int CarEvaluation { get; set; }
        public string CalTime { get; set; }
        public string CalReasonNote { get; set; }
        public decimal OrderAmount { get; set; }
        public decimal PayAmount { get; set; }
        public decimal DynamicDiscountAmount { get; set; }
        public decimal DiscountedAmount { get; set; }
        public string PayTime { get; set; }
        public decimal AmountPayment { get; set; }
        public string AppointmentTime { get; set; }
        public string DriverAcceptOrderTime { get; set; }
        public string DriverStartOffTime { get; set; }
        public string DriverArrivedTime { get; set; }
        public string PassengerGetOnTime { get; set; }
        public double ExpMileage { get; set; }
        public double ExpTime { get; set; }
        public decimal ExpAmount { get; set; }
        public double TotalDistance { get; set; }
        public double TotalTime { get; set; }
        public double EndPointDistance { get; set; }
        public double RealAcceptedTime { get; set; }
    }
}
