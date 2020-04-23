using Abp.Application.Services;
using CarPlusGo.CVAS.Car.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarPlusGo.CVAS.Car
{
    public interface ICarAccessoryAppService: IAsyncCrudAppService<CarAccessoryDto, long, CarAccessoryResultRequestDto, CreateCarAccessoryDto, CreateCarAccessoryDto>
    {
    }
}
