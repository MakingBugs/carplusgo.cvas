using Abp.Application.Services;
using CarPlusGo.CVAS.Car.Dto;

namespace CarPlusGo.CVAS.Car
{
    public interface IClasenAppService
        : IAsyncCrudAppService<ClasenDto, long, PagedClasenResultRequestDto, ClasenDto, ClasenDto>
    {
    }
}
