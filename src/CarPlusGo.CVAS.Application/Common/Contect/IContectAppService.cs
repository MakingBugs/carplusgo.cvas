using Abp.Application.Services;
using CarPlusGo.CVAS.Common.Dto;

namespace CarPlusGo.CVAS.Common
{
    public interface IContectAppService
    : IAsyncCrudAppService<ContectDto, long, PagedContectResultRequestDto, ContectDto, ContectDto>
    {
    }
}