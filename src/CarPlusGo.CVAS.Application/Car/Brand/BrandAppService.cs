using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using CarPlusGo.CVAS.Car.Dto;
using System.Linq;
using System.Threading.Tasks;
using Abp.Extensions;
using Abp.Linq.Extensions;
using System;

namespace CarPlusGo.CVAS.Car
{
    public class BrandAppService
        : AsyncCrudAppService<Brand, BrandDto, long, PagedBrandResultRequestDto, BrandDto, BrandDto>, IBrandAppService
    {
        public BrandAppService(IRepository<Brand, long> repository)
            : base(repository)
        {
        }
        protected override IQueryable<Brand> CreateFilteredQuery(PagedBrandResultRequestDto input)
        {
            return Repository.GetAllIncluding(x => x.FactoryBrand)
                .WhereIf(!input.Keyword.IsNullOrWhiteSpace(), x => x.FactoryBrandAuto.Equals(Convert.ToInt64(input.Keyword)));
        }

        [RemoteService(false)]
        public override Task Delete(EntityDto<long> input)
        {
            return new Task(() => { });
        }
    }
}
