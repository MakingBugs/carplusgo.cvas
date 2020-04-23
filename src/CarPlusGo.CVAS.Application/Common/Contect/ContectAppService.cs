using CarPlusGo.CVAS.Common.Dto;
using Abp.Domain.Repositories;
using Abp.Application.Services;

namespace CarPlusGo.CVAS.Common
{
    public class ContectAppService
        : AsyncCrudAppService<Contect, ContectDto, long, PagedContectResultRequestDto, ContectDto, ContectDto>, IContectAppService
    {
        public ContectAppService(IRepository<Contect, long> repository)
            : base(repository)
        {
        }
    }
}
