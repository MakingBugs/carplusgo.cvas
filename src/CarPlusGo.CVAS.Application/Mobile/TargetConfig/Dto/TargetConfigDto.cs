using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using CarPlusGo.CVAS.Mobile.TShareBank;
using CarPlusGo.CVAS.Mobile.TShareBank.Enum;
using System;

namespace CarPlusGo.CVAS.Mobile.Dto
{
    [AutoMap(typeof(TargetConfig))]
    public class TargetConfigDto : FullAuditedEntityDto<long>
    {
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public decimal OrderAmount { get; set; }
        public long OrderCount { get; set; }
        public long OnlineDriverNum { get; set; }
        public decimal DriverDailyOrderNum { get; set; }
        public long RegisterUserNum { get; set; }
        public long DailyActivityNum { get; set; }
        public Unit Unit { get; set; }
    }
}
