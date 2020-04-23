using Abp.Modules;
using Abp.Reflection.Extensions;
using Abp.Timing;
using Abp.Zero;
using Abp.Zero.Configuration;
using CarPlusGo.CVAS.Authorization.Ldap;
using CarPlusGo.CVAS.Authorization.Ldap.Configuration;
using CarPlusGo.CVAS.Authorization.Roles;
using CarPlusGo.CVAS.Authorization.Users;
using CarPlusGo.CVAS.Configuration;
using CarPlusGo.CVAS.Localization;
using CarPlusGo.CVAS.MultiTenancy;
using CarPlusGo.CVAS.Timing;

namespace CarPlusGo.CVAS
{
    [DependsOn(typeof(AbpZeroCoreModule))]
    public class CVASCoreModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Auditing.IsEnabledForAnonymousUsers = true;

            //AD login config
            IocManager.Register<ILdapSettings, LdapSettings>();
            Configuration.Modules.Zero().UserManagement.ExternalAuthenticationSources.Add<LdapExternalAuthenticationSource>();

            // Declare entity types
            Configuration.Modules.Zero().EntityTypes.Tenant = typeof(Tenant);
            Configuration.Modules.Zero().EntityTypes.Role = typeof(Role);
            Configuration.Modules.Zero().EntityTypes.User = typeof(User);

            CVASLocalizationConfigurer.Configure(Configuration.Localization);

            // Enable this line to create a multi-tenant application.
            Configuration.MultiTenancy.IsEnabled = CVASConsts.MultiTenancyEnabled;

            // Configure roles
            AppRoleConfig.Configure(Configuration.Modules.Zero().RoleManagement);

            Configuration.Settings.Providers.Add<AppSettingProvider>();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(CVASCoreModule).GetAssembly());
        }

        public override void PostInitialize()
        {
            IocManager.Resolve<AppTimes>().StartupTime = Clock.Now;
        }
    }
}
