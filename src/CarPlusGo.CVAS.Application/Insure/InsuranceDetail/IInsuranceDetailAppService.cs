using Abp.Application.Services;
using CarPlusGo.CVAS.Insure.Dto;

namespace CarPlusGo.CVAS.Insure
{
    public interface IInsuranceDetailAppService
        : IAsyncCrudAppService<InsuranceDetailDto, long, PagedInsuranceDetailResultRequestDto, CreateInsuranceDetailDto, UpdateInsuranceDetailDto>
    {
    }
}
