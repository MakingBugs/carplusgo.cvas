using Abp.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarPlusGo.CVAS.Common
{
    [Table("Contect")]
    public class Contect : Entity<long>
    {	
        [Column("Contect_Auto")]
        public override long Id { get; set; }	
        [Column("TradeItem_Auto")]
        public long TradeItemAuto { get; set; }
        public int ContectType { get; set; }
        public string Title { get; set; }
        public string Tel { get; set; }	
        public string ZipCode { get; set; }
        public string Addr { get; set; }
        public string MTEL { get; set; }
        public string FAX { get; set; }
        public int Status { get; set; }
        public int CUser { get; set; }
        public DateTime? CDT { get; set; }
        public int MUser { get; set; }
        public DateTime? MDT { get; set; }
        [Column("Addr_Province")]
        public long AddrProvince { get; set; }	
        [Column("Addr_City")]
        public long AddrCity { get; set; }
        [Column("Addr_Street")]
        public long AddrStreet { get; set; }
        [Column("E_Mail")]
        public string EMail { get; set; }
        public string Dept { get; set; }
        public string AcceptUnit { get; set; }
        public string AcceptTel { get; set; }
    }
}
