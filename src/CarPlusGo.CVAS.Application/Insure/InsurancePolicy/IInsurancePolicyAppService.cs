using Abp.Application.Services;
using CarPlusGo.CVAS.Insure.Dto;

namespace CarPlusGo.CVAS.Insure
{
    public interface IInsurancePolicyAppService 
        : IAsyncCrudAppService<InsurancePolicyDto, long, PagedInsurancePolicyResultRequestDto, InsurancePolicyDto, InsurancePolicyDto>
    {
    }
}
