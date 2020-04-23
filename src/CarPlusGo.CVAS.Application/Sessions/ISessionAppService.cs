using System.Threading.Tasks;
using Abp.Application.Services;
using CarPlusGo.CVAS.Sessions.Dto;

namespace CarPlusGo.CVAS.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
