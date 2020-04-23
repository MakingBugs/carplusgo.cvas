using System.Threading.Tasks;
using CarPlusGo.CVAS.Configuration.Dto;

namespace CarPlusGo.CVAS.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
