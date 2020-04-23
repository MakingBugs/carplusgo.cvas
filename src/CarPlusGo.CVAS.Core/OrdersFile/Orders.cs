using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using CarPlusGo.CVAS.Car;
using CarPlusGo.CVAS.Common;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarPlusGo.CVAS.OrdersFile
{
    [Table("Orders")]
    public class Orders: Entity<long>
    {
        [Column("Orders_Auto")]
        public override long Id { get; set; }
        [ForeignKey("Inc")]
        [Column("Inc_Auto")]
        public long IncAuto { get; set; }
        public Inc Inc { get; set; }
        [ForeignKey("TradeItem")]
        [Column("TradeItem_Auto")]
        public long TradeItemAuto { get; set; }
        public TradeItem TradeItem { get; set; }
        [Column("CarBase_Auto")]
        public long CarBaseAuto { get; set; }
        [Column("Sales_Auto")]
        public long SalesAuto { get; set; }
        public int CustSource { get; set; }
        public string CustMemo { get; set; }
        public int CarSource { get; set; }
        [ForeignKey("FactoryBrand")]
        [Column("FactoryBrand_Auto")]
        public long FactoryBrandAuto { get; set; }
        public FactoryBrand FactoryBrand { get; set; }
        [ForeignKey("Brand")]
        [Column("Brand_Auto")]
        public long BrandAuto { get; set; }
        public Brand Brand { get; set; }
        [ForeignKey("Clasen")]
        [Column("Clasen_Auto")]
        public long ClasenAuto { get; set; }
        public Clasen Clasen { get; set; }
        public string ClasenCode { get; set; }
        public string CarColor { get; set; }
        public DateTime? CarDt { get; set; }
        public int Cc { get; set; }
        public string MakNo { get; set; }
        public decimal ListPrice { get; set; }
        public decimal DisPrice { get; set; }
        public decimal GetPrice { get; set; }
        public decimal Accessary { get; set; }
        public decimal PushMoney { get; set; }
        public string PushMan { get; set; }
        [Column("PushMan_Auto")]
        public int PushManAuto { get; set; }
        public string PushTel { get; set; }
        public decimal CarTax { get; set; }
        public decimal CarCost { get; set; }
        public decimal OverAmt { get; set; }
        public decimal DptAmt { get; set; }
        public int MakNoType { get; set; }
        public decimal MakNoCost { get; set; }
        public int OrderType { get; set; }
        public int Mm { get; set; }
        public decimal RentAmt { get; set; }
        public int RentType { get; set; }
        public decimal StampTax { get; set; }
        public decimal SellCarTax { get; set; }
        public decimal TrnsFee { get; set; }
        public int PayMode { get; set; }
        public int PayDay { get; set; }
        public int InsureType { get; set; }
        public int InsurePercnt { get; set; }
        public int Percnt { get; set; }
        public int CarPlace { get; set; }
        public decimal RateRate { get; set; }
        public decimal FinalRate { get; set; }
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
        public decimal FinalMcost { get; set; }
        public decimal FinalYcost { get; set; }
        public decimal FinalTcost { get; set; }
        public decimal FinalM { get; set; }
        public decimal FinalY { get; set; }
        public decimal FinalT { get; set; }
        public decimal FinalCost { get; set; }
        public decimal FinalDpn { get; set; }
        public decimal FinalTax { get; set; }
        public decimal FinalAmt { get; set; }
        public decimal Mamt { get; set; }
        public decimal? RentAmtT { get; set; }
        public decimal RentRate { get; set; }
        public decimal GrossMargin { get; set; }
        public decimal GrossMarginT { get; set; }
        public int OrderStatus { get; set; }
        public string Memo { get; set; }
        public int TaxMode { get; set; }
        public decimal TaxRate { get; set; }
        public int ChkListPrice { get; set; }
        public int Cuser { get; set; }
        public DateTime Cdt { get; set; }
        public int Muser { get; set; }
        public DateTime Mdt { get; set; }
        public int LinceKm { get; set; }
        public DateTime? AppDate { get; set; }
        public string A1 { get; set; }
        public string A2 { get; set; }
        public string A3 { get; set; }
        public string A4 { get; set; }
        public string A5 { get; set; }
        [Column("Orders_Old")]
        public long OrdersOld { get; set; }
        public int PostType { get; set; }
        public int? IscontractLock { get; set; }
        public long? LockUser { get; set; }
        public DateTime? LockDt { get; set; }
        public int? IsthirdParty { get; set; }
        public int BsType { get; set; }
        public int NoInsure { get; set; }
        public decimal Irr { get; set; }
        public decimal Npv { get; set; }
        public int? IsCredit { get; set; }
        public int? IsCreditUser { get; set; }
        public int DptType { get; set; }
        [Column("Supplier_Buy")]
        public int SupplierBuy { get; set; }
        [Column("Project_Auto")]
        public long? ProjectAuto { get; set; }
        public int? Onetime { get; set; }
        public decimal? RepurchaseAmt { get; set; }
        public int? IsinsureOffer { get; set; }
        public decimal? CostAdjustment { get; set; }
        public int? DptTaxPay { get; set; }
        public decimal? DptTax { get; set; }
        public int? Oil { get; set; }
        public int? IsCustomerCare { get; set; }
        public string ReletMakno { get; set; }
        public int? IsRelet { get; set; }
        public decimal? OnetimeTransfer { get; set; }
        public decimal? OnetimePoundage { get; set; }
        [Column("OverAmt_Y")]
        public decimal? OverAmtY { get; set; }
        [Column("Budget01_Y")]
        public decimal? Budget01Y { get; set; }
        public int? CarTransfer { get; set; }
        [Column("MakNo_INC")]
        public string MakNoInc { get; set; }
        public string Weight { get; set; }
        public int? Mm2 { get; set; }
        public int? UseKm { get; set; }
        public decimal? ServiceAmt { get; set; }
        public decimal? OutFee { get; set; }
        public decimal? FinanceFee { get; set; }
        public decimal? UrgentFee { get; set; }
        public string RequisitionId { get; set; }
        public long? A1size { get; set; }
        public long? A2size { get; set; }
        public long? A3size { get; set; }
        public long? A4size { get; set; }
        public long? A5size { get; set; }
    }
}
