using Abp.Application.Services;
using CarPlusGo.CVAS.Common.Dto;

namespace CarPlusGo.CVAS.Common
{
    public interface ICreditProvinceAppService
        : IAsyncCrudAppService<CreditProvinceDto, long, PagedCreditProvinceResultRequestDto, CreditProvinceDto, CreditProvinceDto>
    {
    }
}