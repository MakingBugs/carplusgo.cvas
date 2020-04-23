using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;

namespace CarPlusGo.CVAS.Common.Dto
{
    [AutoMap(typeof(BankDetail))]
    public class BankDetailDto : EntityDto<long>
    {
        public int? BankType { get; set; }
        public BankType BankTypeData { get; set; }
        public string AreaNumber { get; set; }
        public string BankName { get; set; }
        public string BankNumber { get; set; }
        public int? InVisible { get; set; }
        public long Cuser { get; set; }
        public DateTime Cdt { get; set; }
        public long? Muser { get; set; }
        public DateTime? Mdt { get; set; }
    }
}
