using System.Threading.Tasks;
using Abp.Application.Services;
using CarPlusGo.CVAS.Authorization.Accounts.Dto;

namespace CarPlusGo.CVAS.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
