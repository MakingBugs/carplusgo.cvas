using Abp.Application.Services;
using CarPlusGo.CVAS.Accessories.Dto;

namespace CarPlusGo.CVAS.Accessories
{
    public interface IAccessoriesTypeAppService
        : IAsyncCrudAppService<AccessoriesTypeDto, long, PagedAccessoriesTypeResultRequestDto, CreateOrUpdateAccessoriesTypeDto, CreateOrUpdateAccessoriesTypeDto>
    {
    }
}
