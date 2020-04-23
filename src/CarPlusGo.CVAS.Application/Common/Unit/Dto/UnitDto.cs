using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarPlusGo.CVAS.Common.Dto
{
    [AutoMap(typeof(Unit))]
    public class UnitDto : EntityDto<long>
    {
        public string UnitId { get; set; }
        public string UnitName { get; set; }
        public string UpUnit { get; set; }
        public string LevelId { get; set; }
        public int? LevelNo { get; set; }
        public string HRLevelId { get; set; }
        public string UpUnitId { get; set; }
        public int? IsOn { get; set; }
        public int? Cuser { get; set; }
        public DateTime? CDT { get; set; }
        public int? Muser { get; set; }
        public DateTime? MDT { get; set; }
        public long? IncAuto { get; set; }
        public IncDto Inc { get; set; }
    }
}
