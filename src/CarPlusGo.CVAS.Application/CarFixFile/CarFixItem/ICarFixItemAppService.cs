using Abp.Application.Services;
using CarPlusGo.CVAS.CarFixFile.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarPlusGo.CVAS.CarFixFile
{
    public interface ICarFixItemAppService
         : IAsyncCrudAppService<CarFixItemDto, long, CarFixItemResultRequestDto, CarFixItemDto, CarFixItemDto>
    {
    }
}
