using Abp.Application.Services;
using CarPlusGo.CVAS.Common.Dto;

namespace CarPlusGo.CVAS.Common
{
    public interface IAreaAppService
    : IAsyncCrudAppService<AreaDto, long, PagedAreaResultRequestDto, AreaDto, AreaDto>
    {
    }
}