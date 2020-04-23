using Abp.Application.Services;
using CarPlusGo.CVAS.Mobile.Dto;

namespace CarPlusGo.CVAS.Mobile
{
    public interface ITargetConfigAppService
        : IAsyncCrudAppService<TargetConfigDto, long, PagedTargetConfigResultRequestDto, TargetConfigDto, TargetConfigDto>
    {
    }
}
