using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace CarPlusGo.CVAS.Controllers
{
    public abstract class CVASControllerBase: AbpController
    {
        protected CVASControllerBase()
        {
            LocalizationSourceName = CVASConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
