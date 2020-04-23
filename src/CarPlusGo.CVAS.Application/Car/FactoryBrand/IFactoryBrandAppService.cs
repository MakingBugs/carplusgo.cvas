using Abp.Application.Services;
using CarPlusGo.CVAS.Car.Dto;

namespace CarPlusGo.CVAS.Car
{
    public interface IFactoryBrandAppService
        : IAsyncCrudAppService<FactoryBrandDto, long, PagedFactoryBrandResultRequestDto, FactoryBrandDto, FactoryBrandDto>
    {
    }
}
