using Abp.Application.Services;
using Abp.Domain.Repositories;
using CarPlusGo.CVAS.Common.Dto;
using System.Linq;
using Abp.Linq.Extensions;

namespace CarPlusGo.CVAS.Common
{
    public class AccBankAppService
        : AsyncCrudAppService<AccBank, AccBankDto, long, PagedAccBankResultRequestDto, AccBankDto, AccBankDto>, IAccBankAppService
    {
        public AccBankAppService(IRepository<AccBank, long> repository)
        : base(repository)
        {
        }

        protected override IQueryable<AccBank> CreateFilteredQuery(PagedAccBankResultRequestDto input)
        {
            return Repository.GetAllIncluding(x => x.BankType)
                .Where(x=>x.IsDeleted==false)
                .WhereIf(input.SupplierAuto.HasValue, x => x.SupplierAuto == input.SupplierAuto);
        }
    }
}
