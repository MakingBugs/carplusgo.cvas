using Abp.Application.Services;
using CarPlusGo.CVAS.LocationFile.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarPlusGo.CVAS.LocationFile
{
    public interface ILocationManagerAppService:
        IAsyncCrudAppService<LocationManagerDto, long, LocationManagerResultRequestDto, LocationManagerDto, LocationManagerDto>
    {
    }
}
