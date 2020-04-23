using Abp.Application.Services;
using CarPlusGo.CVAS.Accessories.Dto;

namespace CarPlusGo.CVAS.Accessories
{
    public interface IAccessoriesMainTypeAppService
        : IAsyncCrudAppService<AccessoriesMainTypeDto, long, PagedAccessoriesMainTypeResultRequestDto, AccessoriesMainTypeDto, AccessoriesMainTypeDto>
    {
    }
}
