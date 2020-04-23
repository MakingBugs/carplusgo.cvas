using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;

namespace CarPlusGo.CVAS.Common.Dto
{
    [AutoMap(typeof(IndustryCode))]
    public class IndustryCodeDto : EntityDto<long>
    {
        public string Industrycode { get; set; }
        public string IndustryName { get; set; }
        public string Memo { get; set; }
    }
}
