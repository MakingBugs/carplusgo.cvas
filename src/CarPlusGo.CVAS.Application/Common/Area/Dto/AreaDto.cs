using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;

namespace CarPlusGo.CVAS.Common.Dto
{
    [AutoMap(typeof(Area))]
    public class AreaDto : EntityDto<long>
    {
        public string AreaTitle { get; set; }
        public string AreaMemo { get; set; }
        public int CUser { get; set; }
        public DateTime CDT { get; set; }
        public int MUser { get; set; }
        public DateTime MDT { get; set; }
    }
}
