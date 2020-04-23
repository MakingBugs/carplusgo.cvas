using Abp.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarPlusGo.CVAS.OrderFile
{
    [Table("Order")]
    public partial class Order : Entity<long>
    {   
        [Column("Order_Auto")]
        public override long Id { get; set; }
        [Column("Orders_Auto")]
        public long OrdersAuto { get; set; }
        [Column("TradeItem_Auto")]
        public long TradeItemAuto { get; set; }
        [Column("CarBase_Auto")]
        public long CarBaseAuto { get; set; }
        [Column("Sales_Auto")]
        public long SalesAuto { get; set; }
        public string OrderNo { get; set; }
        public string LinceNo { get; set; }
        public DateTime? StartDt { get; set; }
        public DateTime? EndDt { get; set; }
        public DateTime? ResDt { get; set; }
        public int Status { get; set; }
        public string Memo { get; set; }
        [Column("Brand_Auto")]
        public int BrandAuto { get; set; }
        [Column("Clasen_Auto")]
        public int ClasenAuto { get; set; }
        public decimal ListPrice { get; set; }
        public decimal DisPrice { get; set; }
        public decimal GetPrice { get; set; }
        public decimal Accessary { get; set; }
        public decimal PushMoney { get; set; }
        public decimal CarCost { get; set; }
        public decimal RentAmt { get; set; }
        public decimal OverAmt { get; set; }
        public decimal DptAmt { get; set; }
        public int Mm { get; set; }
        public decimal Mamt { get; set; }
        public string MakNo { get; set; }
        public decimal CarTax { get; set; }
        public int InsurePercnt { get; set; }
        public int IsBusiness { get; set; }
        public int RentType { get; set; }
        public int UseType { get; set; }
        public int CarSource { get; set; }
        public decimal DriverAmt { get; set; }
        public decimal OilAmt { get; set; }
        public decimal DriveService { get; set; }
        public decimal RateKm { get; set; }
        public decimal LinceKm { get; set; }
        public decimal CarMtnAmt { get; set; }
        public int Whiel { get; set; }
        public decimal OverKm { get; set; }
        public int OverKmpayType { get; set; }
        public int PayMode { get; set; }
        public int PayDay { get; set; }
        public decimal RateRate { get; set; }
        public decimal RateInsure { get; set; }
        public decimal RateInsureD { get; set; }
        public decimal RateMcost { get; set; }
        public decimal RateYcost { get; set; }
        public decimal RateTcost { get; set; }
        public decimal RateM { get; set; }
        public decimal RateY { get; set; }
        public decimal RateT { get; set; }
        public decimal RateCost { get; set; }
        public decimal RateDpn { get; set; }
        public decimal RateTax { get; set; }
        public decimal RateAmt { get; set; }
        public decimal FinalRate { get; set; }
        public decimal FinalInsure { get; set; }
        public decimal FinalInsureD { get; set; }
        public decimal FinalMcost { get; set; }
        public decimal FinalYcost { get; set; }
        public decimal FinalTcost { get; set; }
        public decimal FinalM { get; set; }
        public decimal FinalY { get; set; }
        public decimal FinalT { get; set; }
        public decimal FinalCost { get; set; }
        public decimal FinalDpn { get; set; }
        public decimal FinalTax { get; set; }
        public decimal? FinalAmt { get; set; }
        public decimal RentTotal { get; set; }
        public decimal GrossMarginT { get; set; }
        public decimal GrossMargin { get; set; }
        public decimal RentRate { get; set; }
        public int Cuser { get; set; }
        public DateTime Cdt { get; set; }
        public int Muser { get; set; }
        public DateTime Mdt { get; set; }
        public int CustSource { get; set; }
        public decimal Arrange { get; set; }
        public long ArrangeDay { get; set; }
        [Column("Inc_Auto")]
        public long IncAuto { get; set; }
        public decimal RealRate { get; set; }
        public decimal RealGmt { get; set; }
        public decimal RealGm { get; set; }
        public decimal RealRentRate { get; set; }
        public decimal MakNoCost { get; set; }
        public decimal MakNoOverAmt { get; set; }
        public string A1 { get; set; }
        public string B1 { get; set; }
        public string B2 { get; set; }
        public string B3 { get; set; }
        public string B4 { get; set; }
        public int IsChk { get; set; }
        public int OrderType { get; set; }
        public int InvDt { get; set; }
        public int InvStop { get; set; }
        public string InvStopMemo { get; set; }
        public decimal StampTax { get; set; }
        public decimal SellCarTax { get; set; }
        public decimal Irr { get; set; }
        public decimal Npv { get; set; }
        public decimal TrnsFee { get; set; }
        public long NeedFile { get; set; }
        public long MakeFile { get; set; }
        public long NeedFileNum { get; set; }
        public long MakeFileNum { get; set; }
        public decimal RentAmtReal { get; set; }
        [Column("Sub_Auto")]
        public long SubAuto { get; set; }
        public decimal RealCarCost { get; set; }
        public string CollMemo { get; set; }
        public decimal Dpnmamt { get; set; }
        public string IncVirCardNo { get; set; }
        public decimal? RealDptAmt { get; set; }
        public DateTime? RealDptDt { get; set; }
        public int? RealDptIsprinter { get; set; }
        public DateTime? DptPrinterDt { get; set; }
        public string DptRemark { get; set; }
        public string OrderMemo { get; set; }
        public string C1 { get; set; }
        public string C2 { get; set; }
        public string C3 { get; set; }
        public string D1 { get; set; }
        public string D2 { get; set; }
        public string D3 { get; set; }
        public string D4 { get; set; }
        public string D5 { get; set; }
        public int TaxMode { get; set; }
        public decimal TaxRate { get; set; }
        public int ChkListPrice { get; set; }
        public DateTime BoundDt { get; set; }
        public int BoundStatus { get; set; }
        public DateTime? Jcdt { get; set; }
        public int? IsMerit { get; set; }
        public DateTime? RunDt { get; set; }
        public DateTime? Yjcdt { get; set; }
        public int? Yjsales { get; set; }
        public int? Yjorg { get; set; }
        public string Scooter { get; set; }
        public int? DisType { get; set; }
        public DateTime? InvStopEndDt { get; set; }
        public int? IsBonus { get; set; }
        public int? IsRemoveOverdue { get; set; }
        public int? BonusStatus { get; set; }
        public int? BonusYy { get; set; }
        public int? BonusMm { get; set; }
        public decimal? BonusAmt { get; set; }
        public int? IsLawEnd { get; set; }
        public string ReletMakno { get; set; }
        public int? IsRelet { get; set; }
        public DateTime? DptAmtTrnDt { get; set; }
        public decimal? BonusRate { get; set; }
        public decimal? BonusCarCost { get; set; }
        public int? IsBlack { get; set; }
        public DateTime? BlackDt { get; set; }
        public decimal? OutFee { get; set; }
        public decimal? FinanceFee { get; set; }
        public decimal? UrgentFee { get; set; }
    }
}
