using Abp.Application.Services;
using CarPlusGo.CVAS.OrdersFile.Dto;
using System.Threading.Tasks;

namespace CarPlusGo.CVAS.OrdersFile
{
    public interface IOrdersAppService:
        IAsyncCrudAppService<OrdersDto, long, PagedOrdersResultRequestDto , OrdersDto, OrdersDto>
    {
        Task<object> GetOverAmtByClasen(PagedOverAmtResultReuestDto input);

        Task<object> GetRateKMAmtByClasen(PagedRateKmAmtResultRequest input);
    }
}
