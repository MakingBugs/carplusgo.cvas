using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using CarPlusGo.CVAS.Authorization;

namespace CarPlusGo.CVAS
{
    [DependsOn(
        typeof(CVASCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class CVASApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<CVASAuthorizationProvider>();
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(CVASApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddProfiles(thisAssembly)
            );
        }
    }
}
