using Abp.Application.Services;
using Abp.Domain.Repositories;
using CarPlusGo.CVAS.Common.Dto;

namespace CarPlusGo.CVAS.Common
{
    public class IncAppService :
        AsyncCrudAppService<Inc, IncDto, long, PagedIncResultRequestDto, CreateOrUpdateIncDto, CreateOrUpdateIncDto>, IIncAppService
    {
        public IncAppService(IRepository<Inc, long> repository)
            : base(repository)
        {
        }
    }
}
