using Abp.Application.Services;
using CarPlusGo.CVAS.Common.Dto;

namespace CarPlusGo.CVAS.Common
{
    public interface ITradeItemAppService
        : IAsyncCrudAppService<TradeItemDto, long, PagedTradeItemResultRequestDto, TradeItemDto, TradeItemDto>
    {
    }
}
