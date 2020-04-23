using Abp.Application.Services;
using CarPlusGo.CVAS.Mobile.TransportCapacity.OrderMap.Dto;
using System.Threading.Tasks;

namespace CarPlusGo.CVAS.Mobile.TransportCapacity.OrderMap
{
    public interface IOrderMapAppService
        : IApplicationService
    {
        Task<HeatDataDto> HeatData(HeatDataResultRequestDto input);
    }
}
