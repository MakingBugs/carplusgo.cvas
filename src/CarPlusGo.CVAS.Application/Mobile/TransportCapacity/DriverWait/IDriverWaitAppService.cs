using Abp.Application.Services;
using CarPlusGo.CVAS.Mobile.TransportCapacity.DriverWait.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarPlusGo.CVAS.Mobile.TransportCapacity.DriverWait
{
    public interface IDriverWaitAppService : IApplicationService
    {
        Blanket4Dto Blanket(Blanket4ResultRequestDto input);
        Period4Dto Period(Period4ResultRequestDto input);
        Dictionary<string, Period4Dto> ByTheHour(ByTheHour4ResultRequestDto input);
    }
}
