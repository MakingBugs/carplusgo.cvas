using Abp.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarPlusGo.CVAS.Common
{
    /// <summary>
    /// 公司
    /// </summary>
    [Table("Inc")]
    public class Inc : Entity<long>
    {
        [Column("Inc_Auto")]
        public override long Id { get; set; }
        [ForeignKey("TradeItem")]
        [Column("TradeItem_Auto")]
        public long TradeItemAuto { get; set; }
        public TradeItem TradeItem { get; set; }
        public string Fname { get; set; }
        public string Sname { get; set; }
        public string TaxCode { get; set; }
        public int CarTaxMode { get; set; }
        public string AccMemo { get; set; }
        public int Status { get; set; }
        public DateTime Cdt { get; set; }
        public int Cuser { get; set; }
        public DateTime Mdt { get; set; }
        public int Muser { get; set; }
        public string IncVirBankNo { get; set; }
        public string IncVirBankNm { get; set; }
        public string OldBankNo { get; set; }
        public string OldBankNm { get; set; }
        public string LicensePlate { get; set; }
        public string Eascode { get; set; }
        public int? IslimitedLicense { get; set; }
        public int? Area { get; set; }
        [Column("Inc_Addr")]
        public string IncAddr { get; set; }
        [Column("Inc_Tel")]
        public string IncTel { get; set; }
        [Column("Inc_Fax")]
        public string IncFax { get; set; }
        public string CityCode { get; set; }
    }
}
