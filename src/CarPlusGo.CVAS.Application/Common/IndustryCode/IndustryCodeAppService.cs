using CarPlusGo.CVAS.Common.Dto;
using Abp.Domain.Repositories;
using Abp.Application.Services;

namespace CarPlusGo.CVAS.Common
{
    public class IndustryCodeAppService
        : AsyncCrudAppService<IndustryCode, IndustryCodeDto, long, PagedIndustryCodeResultRequestDto, IndustryCodeDto, IndustryCodeDto>, IIndustryCodeAppService
    {
        public IndustryCodeAppService(IRepository<IndustryCode, long> repository)
            : base(repository)
        {
        }
    }
}
