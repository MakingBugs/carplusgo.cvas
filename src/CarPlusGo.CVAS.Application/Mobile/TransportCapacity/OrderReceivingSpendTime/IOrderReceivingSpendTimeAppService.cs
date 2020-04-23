using Abp.Application.Services;
using CarPlusGo.CVAS.Mobile.TransportCapacity.OrderReceivingSpendTime.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarPlusGo.CVAS.Mobile.OrderReceivingSpendTime
{
    public interface IOrderReceivingSpendTimeAppService : IApplicationService
    {
        BlanketDto Blanket(BlanketResultRequestDto input);
        PeriodDto Period(PeriodResultRequestDto input);
        Dictionary<string,PeriodDto> ByTheHour(ByTheHourResultRequestDto input);
    }
}



