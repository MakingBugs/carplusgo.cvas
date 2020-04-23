using Abp.Domain.Entities.Auditing;
using CarPlusGo.CVAS.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CarPlusGo.CVAS.CarFixFile
{
    [Table("CarFix_Batch")]
    public class CarFixBatch : FullAuditedEntity<long>
    {
        [Column("CarFix_Batch_Auto")]
        public override long Id { get; set; }
        public decimal TotalAmt { get; set; }
        [ForeignKey("Supplier")]
        [Column("Supplier_Auto")]
        public long? SupplierAuto { get; set; }
        public Supplier Supplier { get; set; }
        [ForeignKey("Inc")]
        [Column("Inc_Auto")]
        public long? IncAuto { get; set; }
        public Inc Inc { get; set; }
        public int Status { get; set; }
        [ForeignKey("AccBank")]
        [Column("AccBank_Auto")]
        public long? AccBankAuto { get; set; }
        public AccBank AccBank { get; set; }
        public DateTime? PAyDT { get; set; }
        public int IsPAy { get; set; }
        public int BankTab { get; set; }
        public long CUser { get; set; }
        public DateTime CDT { get; set; }
        public long? MUser { get; set; }
        public DateTime? MDT { get; set; }
        public int? RequestStatus { get; set; }
        public string AccountName { get; set; }
        public string AccountBank { get; set; }
        public string BankAccount { get; set; }
        public int? BankType { get; set; }
        [Column("Is_S")]
        public int? IsS { get; set; }
        public int? PayMode { get; set; }
        public long? CarFixBatchTNO { get; set; }
        public int? IsTab { get; set; }
    }
}
