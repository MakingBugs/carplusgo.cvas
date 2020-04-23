using Abp.Domain.Entities.Auditing;
using CarPlusGo.CVAS.Car;
using CarPlusGo.CVAS.Common;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarPlusGo.CVAS.CarFixFile
{
    [Table("CarRepair")]
    public class CarRepair : FullAuditedEntity<long>
    {
        [Column("CarRepair_Auto")]
        public override long Id { get; set; }
        [Column("CarBase_Auto")]
        [ForeignKey("CarBase")]
        public long CarBaseAuto { get; set; }
        public CarBase CarBase { get; set; }
        [Column("Order_Auto")]
        public long OrderAuto { get; set; }
        public DateTime RepairDt { get; set; }
        public long RepairType { get; set; }
        public int Km { get; set; }
        public DateTime RepairDtpre { get; set; }
        public string RepairName { get; set; }
        public string ContactNumber { get; set; }
        public long PayMode { get; set; }
        [Column("Supplier_Auto")]
        [ForeignKey("Supplier")]
        public long SupplierAuto { get; set; }
        public Supplier Supplier { get; set; }
        [Column("System_P")]
        public string SystemP { get; set; }
        public string RepairProblem { get; set; }
        public string OperatingItem { get; set; }
        public string RepairRecommendation { get; set; }
        public decimal EstimatedTimeFee { get; set; }
        public decimal EstimatedPartFee { get; set; }
        public decimal EstimatedTotalFee { get; set; }
        public int Status { get; set; }
        public DateTime? FinishDt { get; set; }
        public int AddStatus { get; set; }
        public long Cuser { get; set; }
        public DateTime Cdt { get; set; }
        public long? Muser { get; set; }
        public DateTime? Mdt { get; set; }
        public int RepairTypeItemType { get; set; }
        [ForeignKey("RepairTypeItemType,RepairType")]
        public ItemCode RepairTypeItemCode { get; set; }
        public int PayModeItemType { get; set; }
        [ForeignKey("PayModeItemType,PayMode")]
        public ItemCode PayModeItemCode { get; set; }
        public int CarFixType { get; set; }
    }
}
