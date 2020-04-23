using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;

namespace CarPlusGo.CVAS.Common.Dto
{
    [AutoMap(typeof(Supplier))]
    public class UpdateDto : FullAuditedEntityDto<long>
    {
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
