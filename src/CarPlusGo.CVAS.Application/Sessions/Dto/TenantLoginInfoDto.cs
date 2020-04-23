using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using CarPlusGo.CVAS.MultiTenancy;

namespace CarPlusGo.CVAS.Sessions.Dto
{
    [AutoMapFrom(typeof(Tenant))]
    public class TenantLoginInfoDto : EntityDto
    {
        public string TenancyName { get; set; }

        public string Name { get; set; }
    }
}
