using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;

namespace CarPlusGo.CVAS.Common.Dto
{
    [AutoMap(typeof(Contect))]
    public class ContectDto : EntityDto<long>
    {
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
        public long AddrProvince { get; set; }
        public long AddrCity { get; set; }
        public long AddrStreet { get; set; }
        public string E_Mail { get; set; }
        public string Dept { get; set; }
        public string AcceptUnit { get; set; }
        public string AcceptTel { get; set; }
    }
}
