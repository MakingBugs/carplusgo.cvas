using Abp.Application.Services;
using CarPlusGo.CVAS.Common.Dto;

namespace CarPlusGo.CVAS.Common
{
    public interface IVEmpAppService
    : IAsyncCrudAppService<VEmpDto, long, PagedVEmpResultRequestDto, VEmpDto, VEmpDto>
    {
    }
}