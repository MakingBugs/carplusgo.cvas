using Abp.Application.Services;
using CarPlusGo.CVAS.Common.Dto;

namespace CarPlusGo.CVAS.Common
{
    public interface ISupplierAppService
        : IAsyncCrudAppService<SupplierDto, long, PagedSupplierResultRequestDto, UpdateDto, UpdateDto>
    {
    }
}
