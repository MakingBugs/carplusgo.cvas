using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using CarPlusGo.CVAS.Car.Dto;
using System.Threading.Tasks;

namespace CarPlusGo.CVAS.Car
{
    public class FactoryBrandAppService
        : AsyncCrudAppService<FactoryBrand, FactoryBrandDto, long, PagedFactoryBrandResultRequestDto, FactoryBrandDto, FactoryBrandDto>, IFactoryBrandAppService
    {
        public FactoryBrandAppService(IRepository<FactoryBrand, long> repository)
            : base(repository)
        {
        }

        [RemoteService(false)]
        public override Task Delete(EntityDto<long> input)
        {
            return new Task(() => { });
        }
    }
}
