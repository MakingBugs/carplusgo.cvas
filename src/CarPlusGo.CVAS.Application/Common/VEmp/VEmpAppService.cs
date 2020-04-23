using CarPlusGo.CVAS.Common.Dto;
using Abp.Domain.Repositories;
using Abp.Application.Services;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using System.Linq;
using Abp.Linq.Extensions;

namespace CarPlusGo.CVAS.Common
{
    public class VEmpAppService
        : AsyncCrudAppService<VEmp, VEmpDto, long, PagedVEmpResultRequestDto, VEmpDto, VEmpDto>, IVEmpAppService
    {
        public VEmpAppService(IRepository<VEmp, long> repository)
            : base(repository)
        {
        }
        protected override IQueryable<VEmp> CreateFilteredQuery(PagedVEmpResultRequestDto input)
        {
            return Repository.GetAll()
                .WhereIf(input.DepCode != null && input.DepCode != "", x => x.DepCode == input.DepCode);

        }
        [RemoteService(false)]
        public override Task Delete(EntityDto<long> input)
        {
            return null;
        }
        [RemoteService(false)]
        public override Task<VEmpDto> Get(EntityDto<long> input)
        {
            return null;
        }
        [RemoteService(false)]
        public override Task<VEmpDto> Create(VEmpDto input)
        {
            return null;
        }

        [RemoteService(false)]
        public override Task<VEmpDto> Update(VEmpDto input)
        {
            return null;
        }
    }
}
