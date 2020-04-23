using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using CarPlusGo.CVAS.Common.Dto;
using System.Threading.Tasks;

namespace CarPlusGo.CVAS.Common
{
    public class TradeItemAppService
        : AsyncCrudAppService<TradeItem, TradeItemDto, long, PagedTradeItemResultRequestDto, TradeItemDto, TradeItemDto>, ITradeItemAppService
    {
        public TradeItemAppService(IRepository<TradeItem, long> repository)
        : base(repository)
        {
        }

        [RemoteService(false)]
        public override Task Delete(EntityDto<long> input)
        {
            return new Task(() => { });
        }
    }
}
