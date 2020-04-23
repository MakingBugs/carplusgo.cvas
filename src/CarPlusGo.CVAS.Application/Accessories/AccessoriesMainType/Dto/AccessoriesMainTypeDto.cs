using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Timing;
using System;

namespace CarPlusGo.CVAS.Accessories.Dto
{
    [AutoMap(typeof(AccessoriesMainType))]
    public class AccessoriesMainTypeDto:EntityDto<long>
    {
        public string AccessoriesMainName { get; set; }
        public long Cuser { get; set; }
        public DateTime Cdt { get; set; }
        public long? Muser { get; set; }
        public DateTime? Mdt { get; set; }
        public AccessoriesMainTypeDto()
        {
            Cdt = Clock.Now;
        }
    }
}
