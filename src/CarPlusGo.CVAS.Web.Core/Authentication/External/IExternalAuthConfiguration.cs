using System.Collections.Generic;

namespace CarPlusGo.CVAS.Authentication.External
{
    public interface IExternalAuthConfiguration
    {
        List<ExternalLoginProviderInfo> Providers { get; }
    }
}
