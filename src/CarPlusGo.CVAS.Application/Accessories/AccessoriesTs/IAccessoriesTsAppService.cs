using Abp.Application.Services;
using CarPlusGo.CVAS.Accessories.Dto;

namespace CarPlusGo.CVAS.Accessories
{
    public interface IAccessoriesTsAppService
        : IAsyncCrudAppService<AccessoriesTsDto, long, PagedAccessoriesTsResultRequestDto, CreateOrUpdateAccessoriesTsDto, CreateOrUpdateAccessoriesTsDto>
    {
    }
}
