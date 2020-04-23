using Abp.Application.Services;
using CarPlusGo.CVAS.Common.Dto;

namespace CarPlusGo.CVAS.Common
{
    public interface IIncAppService :
        IAsyncCrudAppService<IncDto, long, PagedIncResultRequestDto, CreateOrUpdateIncDto, CreateOrUpdateIncDto>
    {
    }
}
