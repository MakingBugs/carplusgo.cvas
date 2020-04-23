using Abp.Application.Services;
using Abp.Domain.Repositories;
using System.Linq;
using Abp.Linq.Extensions;
using CarPlusGo.CVAS.Common.Dto;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;

namespace CarPlusGo.CVAS.Common
{
    public class CreditProvinceAppService
        : AsyncCrudAppService<CreditProvince, CreditProvinceDto, long, PagedCreditProvinceResultRequestDto, CreditProvinceDto, CreditProvinceDto>, ICreditProvinceAppService
    {
        public CreditProvinceAppService(IRepository<CreditProvince, long> repository)
            : base(repository)
        {
        }
        [RemoteService(false)]
        public override Task Delete(EntityDto<long> input)
        {
            return null;
        }
        [RemoteService(false)]
        public override Task<CreditProvinceDto> Get(EntityDto<long> input)
        {
            return null;
        }
        [RemoteService(false)]
        public override Task<CreditProvinceDto> Create(CreditProvinceDto input)
        {
            return null;
        }

        [RemoteService(false)]
        public override Task<CreditProvinceDto> Update(CreditProvinceDto input)
        {
            return null;
        }
    }
}