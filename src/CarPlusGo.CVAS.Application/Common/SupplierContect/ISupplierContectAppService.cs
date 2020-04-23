using Abp.Application.Services;
using CarPlusGo.CVAS.Common.Dto;

namespace CarPlusGo.CVAS.Common
{
    public interface ISupplierContectAppService
        : IAsyncCrudAppService<SupplierContectDto, long, PagedSupplierContectResultRequestDto, SupplierContectDto, SupplierContectDto>
    {
    }
}
