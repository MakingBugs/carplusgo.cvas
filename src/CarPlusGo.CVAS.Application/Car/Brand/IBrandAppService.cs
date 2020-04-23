using Abp.Application.Services;
using CarPlusGo.CVAS.Car.Dto;

namespace CarPlusGo.CVAS.Car
{
    public interface IBrandAppService
        : IAsyncCrudAppService<BrandDto, long, PagedBrandResultRequestDto, BrandDto, BrandDto>
    {
    }
}
