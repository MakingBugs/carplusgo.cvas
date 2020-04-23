using Abp.Application.Services;
using Abp.Domain.Repositories;
using CarPlusGo.CVAS.Common.Dto;

namespace CarPlusGo.CVAS.Common
{
    public class UnitAppService :
        AsyncCrudAppService<Unit, UnitDto, long, UnitResultRequestDto, UnitDto, UnitDto>, IUnitAppService
    {
        public UnitAppService(IRepository<Unit, long> repository)
            : base(repository)
        {
        }
    }
}
