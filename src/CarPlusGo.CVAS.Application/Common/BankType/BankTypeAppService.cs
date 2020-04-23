using Abp.Application.Services;
using Abp.Domain.Repositories;
using CarPlusGo.CVAS.Common.Dto;

namespace CarPlusGo.CVAS.Common
{
    public class BankTypeAppService
         : AsyncCrudAppService<BankType, BankTypeDto, int, PagedBankTypeResultRequestDto, BankTypeDto, BankTypeDto>, IBankTypeAppService
    {
        public BankTypeAppService(IRepository<BankType, int> repository)
        : base(repository)
        {
        }
    }
}
