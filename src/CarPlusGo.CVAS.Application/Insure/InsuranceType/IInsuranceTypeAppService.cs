using Abp.Application.Services;
using CarPlusGo.CVAS.Insure.Dto;

namespace CarPlusGo.CVAS.Insure
{
    public interface IInsuranceTypeAppService
        : IAsyncCrudAppService<InsuranceTypeDto, long, PagedInsuranceTypeResultRequestDto, InsuranceTypeDto, InsuranceTypeDto>
    {
    }
}
