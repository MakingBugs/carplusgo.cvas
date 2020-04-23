using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;

namespace CarPlusGo.CVAS.CarFixFile.Dto
{
    [AutoMap(typeof(AdditionalItem))]
    public class AdditionalItemDto : FullAuditedEntityDto<long>
    {
        public long CarRepairAuto { get; set; }
        public string AdditionalItem1 { get; set; }
        public int Status { get; set; }
        public int SerialNumber { get; set; }
        public long Cuser { get; set; }
        public DateTime Cdt { get; set; }
        public long? Muser { get; set; }
        public DateTime? Mdt { get; set; }
    }
}
