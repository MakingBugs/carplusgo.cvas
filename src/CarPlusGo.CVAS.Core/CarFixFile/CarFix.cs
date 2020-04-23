using Abp.Domain.Entities.Auditing;
using CarPlusGo.CVAS.Car;
using CarPlusGo.CVAS.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarPlusGo.CVAS.CarFixFile
{
    [Table("CarFix")]
    public class CarFix : FullAuditedEntity<long>
    {
        [Column("CarFix_Auto")]
        public override long Id { get; set; }
        [ForeignKey("CarBase")]
        [Column("CarBase_Auto")]
        public long CarBaseAuto { get; set; }
        public CarBase CarBase { get; set; }
        [Column("Order_Auto")]
        public long OrderAuto { get; set; }
        [ForeignKey("Supplier")]
        [Column("Supplier_Auto")]
        public long? SupplierAuto { get; set; }
        public Supplier Supplier { get; set; }
        public string CarFixNo { get; set; }
        public string MakNo { get; set; }
        public string CustName { get; set; }
        public DateTime FixDt { get; set; }
        public DateTime FixDtpre { get; set; }
        public DateTime FixDtreal { get; set; }
        public int Km { get; set; }
        public long? FixType { get; set; }
        public int? ItemCodeFixType { get; set; }
        [ForeignKey("ItemCodeFixType,FixType")]
        public ItemCode ItemCodeFixTypeData { get; set; }
        public int MainTainKm { get; set; }
        public decimal Whamount { get; set; }
        public decimal WhdisCount { get; set; }
        public decimal ItemAmount { get; set; }
        public decimal ItemDisCount { get; set; }
        public decimal RealAmount { get; set; }
        public long? Status { get; set; }
        public int? ItemCodeStatus { get; set; }
        [ForeignKey("ItemCodeStatus,Status")]
        public ItemCode ItemCodeStatusData { get; set; }
        public int Cuser { get; set; }
        public DateTime Cdt { get; set; }
        public int Muser { get; set; }
        public DateTime Mdt { get; set; }
        [ForeignKey("AccBank")]
        [Column("AccBank_Auto")]
        public long? AccBankAuto { get; set; }
        public AccBank AccBank { get; set; }
        [ForeignKey("CarFixBatch")]
        [Column("CarFix_Batch_Auto")]
        public long? CarFixBatchAuto { get; set; }
        public CarFixBatch CarFixBatch { get; set; }
        public string Remark { get; set; }
        public long? CarFixBatchTno { get; set; }
        public int? PayMode { get; set; }
        [Column("CarRepair_Auto")]
        public int? CarRepairAuto { get; set; }
        public int? NextMaintainKm { get; set; }
        public DateTime? NextMaintainDt { get; set; }
        public DateTime? PreNextMaintainDt { get; set; }

        [ForeignKey("CarFixAuto")]
        public virtual ICollection<CarFixItem> CarFixItem { get; set; }
        [ForeignKey("SourceAuto")]
        public virtual ICollection<PRInvLink> PRInvLink { get; set; }
    }
}
