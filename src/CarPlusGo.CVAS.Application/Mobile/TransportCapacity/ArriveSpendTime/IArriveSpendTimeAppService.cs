using Abp.Application.Services;
using CarPlusGo.CVAS.Mobile.TransportCapacity.ArriveSpendTime.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarPlusGo.CVAS.Mobile.TransportCapacity.ArriveSpendTime
{
    public interface IArriveSpendTimeAppService : IApplicationService
    {
        Blanket2Dto Blanket(Blanket2ResultRequestDto input);
    }
}
