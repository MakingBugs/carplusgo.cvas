using CarPlusGo.CVAS.Common.Dto;
using Abp.Domain.Repositories;
using Abp.Application.Services;

namespace CarPlusGo.CVAS.Common
{
    public class AreaAppService
        : AsyncCrudAppService<Area, AreaDto, long, PagedAreaResultRequestDto, AreaDto, AreaDto>, IAreaAppService
    {
        public AreaAppService(IRepository<Area, long> repository)
            : base(repository)
        {
        }
    }
}
