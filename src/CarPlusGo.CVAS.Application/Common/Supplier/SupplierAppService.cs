using Abp.Application.Services;
using Abp.Domain.Repositories;
using CarPlusGo.CVAS.Common.Dto;
using System.Linq;
using Abp.Extensions;
using Abp.Linq.Extensions;

namespace CarPlusGo.CVAS.Common
{
    public class SupplierAppService
        : AsyncCrudAppService<Supplier, SupplierDto, long, PagedSupplierResultRequestDto, UpdateDto, UpdateDto>, ISupplierAppService
    {
        public SupplierAppService(IRepository<Supplier, long> repository)
            : base(repository)
        {
        }

        protected override IQueryable<Supplier> CreateFilteredQuery(PagedSupplierResultRequestDto input)
        {
            return Repository.GetAll()
                .Where(x=>x.IsDeleted==false)
                .WhereIf(input.SupplierAuto.HasValue, x => x.Id == input.SupplierAuto)
                .WhereIf(!input.Key.IsNullOrEmpty(), x => x.Fname.Contains(input.Key))
                .WhereIf(input.SupplierTypes.Length > 0, x => input.SupplierTypes.Any(s => x.SupplierT.Contains(s + ",")));
        }
    }
}
