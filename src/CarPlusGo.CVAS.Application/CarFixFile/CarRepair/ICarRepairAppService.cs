using Abp.Application.Services;
using CarPlusGo.CVAS.CarFixFile.Dto;

namespace CarPlusGo.CVAS.CarFixFile
{
    public interface ICarRepairAppService
        : IAsyncCrudAppService<CarRepairDto, long, CarRepairResultRequestDto, CarRepairDto, CarRepairDto>
    {
    }
}
