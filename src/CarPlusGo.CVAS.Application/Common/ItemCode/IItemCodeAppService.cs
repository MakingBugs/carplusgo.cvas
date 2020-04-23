using Abp.Application.Services;
using CarPlusGo.CVAS.Common.Dto;

namespace CarPlusGo.CVAS.Common
{
    public interface IItemCodeAppService :
        IAsyncCrudAppService<ItemCodeDto, long, PagedItemCodeResultRequestDto, ItemCodeDto, ItemCodeDto>
    {
    }
}
