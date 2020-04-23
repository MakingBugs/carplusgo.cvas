using Abp.Application.Services;
using CarPlusGo.CVAS.Common.Dto;

namespace CarPlusGo.CVAS.Common
{
    public interface ICreditCityAppService
    : IAsyncCrudAppService<CreditCityDto, long, PagedCreditCityResultRequestDto, CreditCityDto, CreditCityDto>
    {
    }
}