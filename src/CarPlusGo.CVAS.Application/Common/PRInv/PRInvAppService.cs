using Abp.Application.Services;
using Abp.Domain.Repositories;
using CarPlusGo.CVAS.Common.Dto;
using System.Linq;
using Abp.Linq.Extensions;
using System.Text;

namespace CarPlusGo.CVAS.Common
{
    public class PRInvAppService
            : AsyncCrudAppService<PRInv, PRInvDto, long, PRInvResultRequestDto, PRInvDto, PRInvDto>, IPRInvAppService
    {
        public PRInvAppService(IRepository<PRInv, long> repository)
           : base(repository)
        {
        }
        protected override IQueryable<PRInv> CreateFilteredQuery(PRInvResultRequestDto input)
        {
            return Repository.GetAllIncluding(x => x.PRInvLink,x=>x.Inc,x=>x.Supplier,x=>x.BankTypeData)
                .Where(x => x.IsDeleted == false)
                .WhereIf(input.Amount.HasValue,x=>x.Amount==input.Amount)
                .WhereIf(input.SupplierAuto.HasValue,x=>x.SupplierAuto==input.SupplierAuto)
                .WhereIf(input.InvNo != null && input.InvNo != "", x => x.InvNo.Contains(input.InvNo))
                .WhereIf(input.PrinvLinkAuto.HasValue, x => x.PrinvLinkAuto == input.PrinvLinkAuto);
        } 
    }
}
