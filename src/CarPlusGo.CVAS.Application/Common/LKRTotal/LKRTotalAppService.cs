using Abp.Application.Services;
using Abp.Domain.Repositories;
using CarPlusGo.CVAS.Common.Dto;
using System.Linq;
using Abp.Linq.Extensions;
using System.Text;

namespace CarPlusGo.CVAS.Common
{
    public class LKRTotalAppService
            : AsyncCrudAppService<LKRTotal, LKRTotalDto, long, LKRTotalResultRequestDto, LKRTotalDto, LKRTotalDto>, ILKRTotalAppService
    {
        public LKRTotalAppService(IRepository<LKRTotal, long> repository)
           : base(repository)
        {
        }
        protected override IQueryable<LKRTotal> CreateFilteredQuery(LKRTotalResultRequestDto input)
        {
            return Repository.GetAllIncluding(x => x.BankType)
                .WhereIf(input.LKRName != null && input.LKRName != "", x => x.LKRName.Contains(input.LKRName));
        }
    }
}
