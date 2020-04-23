using Abp.Application.Services;
using CarPlusGo.CVAS.Mobile.Dto;

namespace CarPlusGo.CVAS.Mobile
{
    public interface IOperationTargetAppService
        : IAsyncCrudAppService<OperationTargetDto, long, PagedOperationTargetResultRequestDto, OperationTargetDto, OperationTargetDto>
    {
    }
}
