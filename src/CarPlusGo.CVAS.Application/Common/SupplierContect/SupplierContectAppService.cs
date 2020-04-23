using Abp.Application.Services;
using Abp.Domain.Repositories;
using CarPlusGo.CVAS.Common.Dto;
using System.Linq;
using Abp.Linq.Extensions;

namespace CarPlusGo.CVAS.Common
{
    public class SupplierContectAppService
        : AsyncCrudAppService<SupplierContect, SupplierContectDto, long, PagedSupplierContectResultRequestDto, SupplierContectDto, SupplierContectDto>, ISupplierContectAppService
    {
        public SupplierContectAppService(IRepository<SupplierContect, long> repository)
            : base(repository)
        {
        }
        protected override IQueryable<SupplierContect> CreateFilteredQuery(PagedSupplierContectResultRequestDto input)
        {
            return Repository.GetAllIncluding(x => x.CreditProvince, x => x.CreditCity, x => x.CreditArea)
            .Where(x=>x.IsDeleted==false)
            .WhereIf(input.SupplierAuto.HasValue, x => x.SupplierAuto == input.SupplierAuto);
        }
    }
}
