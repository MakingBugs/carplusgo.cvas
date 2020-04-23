using Abp.Application.Services;
using CarPlusGo.CVAS.Insure.Dto;

namespace CarPlusGo.CVAS.Insure
{
    public interface IInsurancePresetAppService
        : IAsyncCrudAppService<InsurancePresetDto, long, PagedInsurancePresetResultRequestDto, CreateOrUpdateInsurancePresetDto, CreateOrUpdateInsurancePresetDto>
    {
    }
}
