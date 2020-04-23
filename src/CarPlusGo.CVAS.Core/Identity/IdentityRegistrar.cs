using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using CarPlusGo.CVAS.Authorization;
using CarPlusGo.CVAS.Authorization.Roles;
using CarPlusGo.CVAS.Authorization.Users;
using CarPlusGo.CVAS.Editions;
using CarPlusGo.CVAS.MultiTenancy;

namespace CarPlusGo.CVAS.Identity
{
    public static class IdentityRegistrar
    {
        public static IdentityBuilder Register(IServiceCollection services)
        {
            services.AddLogging();

            return services.AddAbpIdentity<Tenant, User, Role>()
                .AddAbpTenantManager<TenantManager>()
                .AddAbpUserManager<UserManager>()
                .AddAbpRoleManager<RoleManager>()
                .AddAbpEditionManager<EditionManager>()
                .AddAbpUserStore<UserStore>()
                .AddAbpRoleStore<RoleStore>()
                .AddAbpLogInManager<LogInManager>()
                .AddAbpSignInManager<SignInManager>()
                .AddAbpSecurityStampValidator<SecurityStampValidator>()
                .AddAbpUserClaimsPrincipalFactory<UserClaimsPrincipalFactory>()
                .AddPermissionChecker<PermissionChecker>()
                .AddDefaultTokenProviders();
        }
    }
}
