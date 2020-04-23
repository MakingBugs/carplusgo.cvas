using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using CarPlusGo.CVAS.Configuration;

namespace CarPlusGo.CVAS.Web.Host.Startup
{
    [DependsOn(
       typeof(CVASWebCoreModule))]
    public class CVASWebHostModule: AbpModule
    {
        private readonly IHostingEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public CVASWebHostModule(IHostingEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(CVASWebHostModule).GetAssembly());
        }
    }
}
