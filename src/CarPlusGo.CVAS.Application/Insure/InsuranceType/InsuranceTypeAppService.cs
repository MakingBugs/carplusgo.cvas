using Abp.Application.Services;
using Abp.Domain.Repositories;
using CarPlusGo.CVAS.Insure.Dto;
using System.Linq;
using Abp.Extensions;
using Abp.Linq.Extensions;

namespace CarPlusGo.CVAS.Insure
{
    public class InsuranceTypeAppService
        : AsyncCrudAppService<InsuranceType, InsuranceTypeDto, long, PagedInsuranceTypeResultRequestDto, InsuranceTypeDto, InsuranceTypeDto>, IInsuranceTypeAppService
    {
        public InsuranceTypeAppService(IRepository<InsuranceType, long> repository)
        : base(repository)
        {
        }

        protected override IQueryable<InsuranceType> CreateFilteredQuery(PagedInsuranceTypeResultRequestDto input)
        {
            return Repository.GetAll()
                .WhereIf(!input.Keyword.IsNullOrWhiteSpace(), x => x.Name.Contains(input.Keyword))
                .WhereIf(input.IsActive.HasValue, x => x.IsActive == input.IsActive);
        }
    }
}
