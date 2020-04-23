using Abp.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarPlusGo.CVAS.Common
{
    /// <summary>
    /// 对象表
    /// </summary>
    [Table("TradeItem")]
    public class TradeItem : Entity<long>
    {
        [Column("TradeItem_Auto")]
        public override long Id { get; set; }
        [Column("Area_Auto")]
        public long AreaAuto { get; set; }
        public string Fid { get; set; }
        public string Fname { get; set; }
        public string Sname { get; set; }
        public DateTime? IncCdt { get; set; }
        public string Tel { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string Addr { get; set; }
        public string ZipCode { get; set; }
        public int Cuser { get; set; }
        public DateTime Cdt { get; set; }
        public int Muser { get; set; }
        public DateTime Mdt { get; set; }
        public byte[] Ver { get; set; }
        public string Bank { get; set; }
        public string Account { get; set; }
        public string Source { get; set; }
        public long AddrProvince { get; set; }
        public long AddrCity { get; set; }
        public long AddrArea { get; set; }
        public string TaxId { get; set; }
        public string BankNo { get; set; }
        public string CreditedName { get; set; }
        public string CreditedTel { get; set; }
        public string OrgCode { get; set; }
        public string Mtel { get; set; }
        public string IndustryCode { get; set; }
        public int? IsSendInv { get; set; }
        public int? BankName { get; set; }
        public string CredentialNo { get; set; }
        public string Iscollection { get; set; }
        public int? RemakeType { get; set; }
        public int? IsEas { get; set; }
        [Column("RecallRec_Auto")]
        public long? RecallRecAuto { get; set; }
        [Column("LastOrder_Auto")]
        public long? LastOrderAuto { get; set; }
    }
}
