using Abp.Application.Services;
using CarPlusGo.CVAS.Car.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarPlusGo.CVAS.Car
{
    public interface ICarMemoAppService
    : IAsyncCrudAppService<CarMemoDto, long, PagedCarMemoResultRequestDto, CarMemoDto, CarMemoDto>
    {
    }
}
