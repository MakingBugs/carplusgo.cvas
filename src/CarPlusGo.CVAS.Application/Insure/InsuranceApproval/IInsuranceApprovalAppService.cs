using Abp.Application.Services;
using CarPlusGo.CVAS.Insure.Dto;

namespace CarPlusGo.CVAS.Insure
{
    public interface IInsuranceApprovalAppService
        : IAsyncCrudAppService<InsuranceApprovalDto, long, PagedInsuranceApprovalResultRequestDto, CreateInsuranceApprovalDto, InsuranceApprovalDto>
    {
    }
}
