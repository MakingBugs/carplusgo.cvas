using Abp.Application.Services;
using Abp.Domain.Repositories;
using CarPlusGo.CVAS.Common.Dto;
using System.Linq;
using Abp.Linq.Extensions;
using Abp.Extensions;

namespace CarPlusGo.CVAS.Common
{
    public class BankDetailAppService
        : AsyncCrudAppService<BankDetail, BankDetailDto, long, PagedBankDetailResultRequestDto, BankDetailDto, BankDetailDto>, IBankDetailAppService
    {
        public BankDetailAppService(IRepository<BankDetail, long> repository)
        : base(repository)
        {
        }

        protected override IQueryable<BankDetail> CreateFilteredQuery(PagedBankDetailResultRequestDto input)
        {
            return Repository.GetAllIncluding(x=>x.BankTypeData)
                .WhereIf(!input.Keyword.IsNullOrWhiteSpace(), x => x.BankName.Contains(input.Keyword))
                .WhereIf(input.BankType.HasValue, x => x.BankType == input.BankType);
        }
    }
}
