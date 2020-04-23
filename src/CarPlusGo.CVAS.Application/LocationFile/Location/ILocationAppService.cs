﻿using Abp.Application.Services;
using CarPlusGo.CVAS.LocationFile.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarPlusGo.CVAS.LocationFile
{
    public interface ILocationAppService:
        IAsyncCrudAppService<LocationDto, long, LocationResultRequestDto, LocationDto, LocationDto>
    {
    }
}
