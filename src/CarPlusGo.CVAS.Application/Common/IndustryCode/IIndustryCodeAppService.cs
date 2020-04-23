using Abp.Application.Services;
using CarPlusGo.CVAS.Common.Dto;

namespace CarPlusGo.CVAS.Common
{
    public interface IIndustryCodeAppService
    : IAsyncCrudAppService<IndustryCodeDto, long, PagedIndustryCodeResultRequestDto, IndustryCodeDto, IndustryCodeDto>
    {
    }
}