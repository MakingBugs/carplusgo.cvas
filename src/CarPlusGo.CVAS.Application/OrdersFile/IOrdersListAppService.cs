using Abp.Application.Services;
using Abp.Application.Services.Dto;
using CarPlusGo.CVAS.OrdersFile.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CarPlusGo.CVAS.OrdersFile
{
    public interface IOrdersListAppService 
        : IAsyncCrudAppService<OrdersListDto, long, PagedOrdersListResultRequestDto>
    {
        Task<PagedResultDto<OrdersListDto>> GetOrdersList(PagedOrdersListResultRequestDto input);
    }
}
