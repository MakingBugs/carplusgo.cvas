using Abp.Application.Services;
using CarPlusGo.CVAS.Mobile.TransportCapacity.PassengerWait.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarPlusGo.CVAS.Mobile.TransportCapacity.PassengerWait
{
    public interface IPassengerWaitAppService:IApplicationService
    {
        Blanket3Dto Blanket(Blanket3ResultRequestDto input);
        Period3Dto Period(Period3ResultRequestDto input);
        Dictionary<string, Period3Dto> ByTheHour(ByTheHour3ResultRequestDto input);
    }
}
