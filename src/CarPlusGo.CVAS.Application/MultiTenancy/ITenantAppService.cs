using Abp.Application.Services;
using CarPlusGo.CVAS.MultiTenancy.Dto;

namespace CarPlusGo.CVAS.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}

