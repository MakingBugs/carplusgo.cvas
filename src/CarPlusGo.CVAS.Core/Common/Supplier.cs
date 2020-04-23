using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarPlusGo.CVAS.Common
{
    /// <summary>
    /// 厂商
    /// </summary>
    [Table("Supplier")]
    public class Supplier : FullAuditedEntity<long>
    {
        [Column("Supplier_Auto")]
        public override long Id { get; set; }
        public TradeItem TradeItem { get; set; }
        [ForeignKey("TradeItem")]
        [Column("TradeItem_Auto")]
        public long TradeItemAuto { get; set; }
        public string SupplierCode { get; set; }
        public int SupplierType { get; set; }
        public int IncType { get; set; }
        public DateTime? LinceBeginDt { get; set; }
        public DateTime? LinceEndDt { get; set; }
        public int ServiceClasen { get; set; }
        public int Whdiscount { get; set; }
        public int ItemDiscount { get; set; }
        public string BankName { get; set; }
        public string Account { get; set; }
        public string AccountName { get; set; }
        public int PayMode { get; set; }
        public int PayDt { get; set; }
        public int PayDay { get; set; }
        public int Status { get; set; }
        public int Cuser { get; set; }
        public DateTime Cdt { get; set; }
        public int Muser { get; set; }
        public DateTime Mdt { get; set; }
        [Column("Supplier_T")]
        public string SupplierT { get; set; }
        public string Fid { get; set; }
        public string Fname { get; set; }
        public string Sname { get; set; }
        public DateTime? IncCdt { get; set; }
        public string Tel { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string Addr { get; set; }
        public string ZipCode { get; set; }
        public long? AddrProvince { get; set; }
        public long? AddrCity { get; set; }
        public long? AddrArea { get; set; }
    }
}
