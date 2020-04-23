using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities.Auditing;
using System;

namespace CarPlusGo.CVAS.Common.Dto
{
    [AutoMap(typeof(AccBank))]
    public class AccBankDto : FullAuditedEntityDto<long>
    {
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
        public int BankTypeId { get; set; }
        public long BankDetailAuto { get; set; }
        public BankType BankType { get; set; }
    }
}
