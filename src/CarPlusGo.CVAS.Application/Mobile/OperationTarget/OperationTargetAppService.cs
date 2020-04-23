using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using CarPlusGo.CVAS.Mobile.Dto;
using CarPlusGo.CVAS.Mobile.TShareBank;
using System.Linq;
using Abp.Extensions;
using Abp.Linq.Extensions;
using System.Threading.Tasks;

namespace CarPlusGo.CVAS.Mobile
{
    public class OperationTargetAppService
        : AsyncCrudAppService<OperationTarget, OperationTargetDto, long, PagedOperationTargetResultRequestDto, OperationTargetDto, OperationTargetDto>, IOperationTargetAppService
    {
        public OperationTargetAppService(IRepository<OperationTarget, long> repository)
            : base(repository)
        {
        }

        protected override IQueryable<OperationTarget> CreateFilteredQuery(PagedOperationTargetResultRequestDto input)
        {
            return Repository.GetAll()
                .WhereIf(input.From.HasValue && input.To.HasValue, x => x.Date >= input.From.Value.ToLocalTime().Date && x.Date <= input.To.Value.ToLocalTime().ToDayEnd());
        }

        [RemoteService(false)]
        public override Task Delete(EntityDto<long> input)
        {
            return null;
        }

        [RemoteService(false)]
        public override Task<OperationTargetDto> Get(EntityDto<long> input)
        {
            return null;
        }

        [RemoteService(false)]
        public override Task<OperationTargetDto> Create(OperationTargetDto input)
        {
            return null;
        }
    }
}
