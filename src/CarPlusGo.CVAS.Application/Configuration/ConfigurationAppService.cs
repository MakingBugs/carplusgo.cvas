using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using CarPlusGo.CVAS.Configuration.Dto;

namespace CarPlusGo.CVAS.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : CVASAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
