using Abp.Application.Services;
using CarPlusGo.CVAS.Car.Dto;

namespace CarPlusGo.CVAS.Car
{
    public interface ICarBaseAppService
        : IAsyncCrudAppService<CarBaseDto, long, PagedCarBaseResultRequestDto, UpdateCarBaseDto, UpdateCarBaseDto>
    {
    }
}
