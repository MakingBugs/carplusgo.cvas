using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarPlusGo.CVAS.Common
{
    [Table("AccBank")]
    public class AccBank : FullAuditedEntity<long>
    {
        [Column("AccBank_Auto")]
        public override long Id { get; set; }
        [Column("Supplier_Auto")]
        public long SupplierAuto { get; set; }
        public int Seq { get; set; }
        public string BankName { get; set; }
        public string Account { get; set; }
        public string AccountName { get; set; }
        public int Cuser { get; set; }
        public DateTime Cdt { get; set; }
        public int Muser { get; set; }
        public DateTime Mdt { get; set; }
        public string Memo { get; set; }
        public int IsUser { get; set; }
        [Column("BankType")]
        [ForeignKey("BankType")]
        public int BankTypeId { get; set; }
        public long BankDetailAuto { get; set; }
        public BankType BankType { get; set; }
    }
}
