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
    public class ClasenAppService
        : AsyncCrudAppService<Clasen, ClasenDto, long, PagedClasenResultRequestDto, ClasenDto, ClasenDto>, IClasenAppService
    {
        public ClasenAppService(IRepository<Clasen, long> repository)
            : base(repository)
        {
        }

        protected override IQueryable<Clasen> CreateFilteredQuery(PagedClasenResultRequestDto input)
        {
            return Repository.GetAllIncluding(x => x.Brand)
                .WhereIf(!input.Keyword.IsNullOrWhiteSpace(), x => x.BrandAuto.Equals(Convert.ToInt64(input.Keyword)));
        }

        [RemoteService(false)]
        public override Task Delete(EntityDto<long> input)
        {
            return new Task(() => { });
        }
    }
}
