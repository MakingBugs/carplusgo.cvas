using Abp.Application.Services;
using Abp.Application.Services.Dto;
using CarPlusGo.CVAS.OrdersFeeTypeFile.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CarPlusGo.CVAS.OrdersFeeTypeFile
{
    public interface IOrderFeeTypeAppService :
        IAsyncCrudAppService<OrdersFeeTypeDto, long, PagedOrdersFeeTypeResultRequestDto, OrdersFeeTypeDto, OrdersFeeTypeDto>
    {
        Task<OrdersFeeTypeDto> GetOrdersFeeTypeByID(EntityDto<long> input);
        Task<PagedResultDto<OrdersFeeTypeDto>> GetOrdersFeeTypeByInc(PagedOrdersFeeTypeResultRequestDto input);
         
    }
}
