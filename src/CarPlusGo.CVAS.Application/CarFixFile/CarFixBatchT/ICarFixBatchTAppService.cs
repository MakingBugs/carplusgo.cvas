using Abp.Application.Services;
using CarPlusGo.CVAS.CarFixFile.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarPlusGo.CVAS.CarFixFile
{
    public interface ICarFixBatchTAppService
        : IAsyncCrudAppService<CarFixBatchTDto, long, CarFixBatchTResultRequestDto, CarFixBatchTDto, CarFixBatchTDto>
    {
    }
}
