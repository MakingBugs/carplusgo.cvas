using System;
using System.Threading.Tasks;
using Abp;
using Abp.Authorization.Users;
using Abp.Dependency;
using Abp.Extensions;
using CarPlusGo.CVAS.Authorization.Ldap.Configuration;
using CarPlusGo.CVAS.Authorization.Users;
using CarPlusGo.CVAS.MultiTenancy;
using Novell.Directory.Ldap;

namespace CarPlusGo.CVAS.Authorization.Ldap
{
    public class LdapExternalAuthenticationSource : DefaultExternalAuthenticationSource<Tenant, User>, ITransientDependency
    {
        /// <summary>
        /// LDAP
        /// </summary>
        public const string SourceName = "LDAP";

        public override string Name => SourceName;

        private readonly ILdapSettings _settings;

        public LdapExternalAuthenticationSource(ILdapSettings settings)
        {
            _settings = settings;
        }

        public override async Task<bool> TryAuthenticateAsync(string userNameOrEmailAddress, string plainPassword, Tenant tenant)
        {
            if (!(await _settings.GetIsEnabled(tenant?.Id)))
            {
                return false;
            }

            return await ValidateCredentials(userNameOrEmailAddress.ToLower(), plainPassword, tenant);
        }

        private async Task<bool> ValidateCredentials(string userNameOrEmailAddress, string plainPassword, Tenant tenant)
        {
            var searchFilter = await CreateSearchFilter(userNameOrEmailAddress, tenant);
            var attrs = new string[] { "sAMAccountName" };

            try
            {
                using (var ldapConnection = await CreateLdapConnection(tenant, userNameOrEmailAddress))
                {
                    var entities = ldapConnection.Search(await _settings.GetContainer(tenant?.Id), LdapConnection.SCOPE_SUB, searchFilter, attrs, false);
                    string userDn = null;
                    while (entities.HasMore())
                    {
                        var entity = entities.Next();
                        var account = entity.getAttribute("sAMAccountName");
                        if (account != null && account.StringValue == userNameOrEmailAddress)
                        {
                            userDn = entity.DN;
                            break;
                        }
                    }
                    if (string.IsNullOrWhiteSpace(userDn)) return false;
                    ldapConnection.Bind(userDn, plainPassword);
                    ldapConnection.Disconnect();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public override async Task<User> CreateUserAsync(string userNameOrEmailAddress, Tenant tenant)
        {
            await CheckIsEnabled(tenant);

            var user = await base.CreateUserAsync(userNameOrEmailAddress, tenant);

            using (var ldapConnection = await CreateLdapConnection(tenant, userNameOrEmailAddress))
            {
                UpdateUserFromLdapConnection(ldapConnection, user, userNameOrEmailAddress, tenant);

                user.IsEmailConfirmed = true;
                user.IsActive = true;

                return user;
            }
        }

        public override async Task UpdateUserAsync(User user, Tenant tenant)
        {
            await CheckIsEnabled(tenant);

            await base.UpdateUserAsync(user, tenant);

            using (var ldapConnection = await CreateLdapConnection(tenant, user))
            {
                UpdateUserFromLdapConnection(ldapConnection, user, user.UserName, tenant);
            }
        }

        protected virtual async void UpdateUserFromLdapConnection(ILdapConnection ldapConnection, User user, string userNameOrEmailAddress, Tenant tenant)
        {

            var searchFilter = await CreateSearchFilter(userNameOrEmailAddress, tenant);
            var attrs = new string[] { "sAMAccountName", "mail", "sn" };

            var entities = ldapConnection.Search(await _settings.GetContainer(tenant?.Id), LdapConnection.SCOPE_SUB, searchFilter, attrs, false);
            while (entities.HasMore())
            {
                var entity = entities.Next();
                var account = entity.getAttribute("sAMAccountName");
                if (account != null && account.StringValue == userNameOrEmailAddress)
                {
                    if (entity.getAttribute("sAMAccountName") != null && !entity.getAttribute("sAMAccountName").StringValue.IsNullOrEmpty())
                    {
                        user.UserName = entity.getAttribute("sAMAccountName").StringValue;
                        user.Name = entity.getAttribute("sAMAccountName").StringValue;
                    }

                    user.Surname = entity.getAttribute("sn") != null ? entity.getAttribute("sn").StringValue : user.Surname;
                    user.EmailAddress = entity.getAttribute("mail") != null ? entity.getAttribute("mail").StringValue : user.EmailAddress;
                    break;
                }
            }
        }

        protected virtual Task<LdapConnection> CreateLdapConnection(Tenant tenant, string userNameOrEmailAddress)
        {
            return CreateLdapConnection(tenant);
        }

        protected virtual Task<LdapConnection> CreateLdapConnection(Tenant tenant, User user)
        {
            return CreateLdapConnection(tenant);
        }

        protected virtual async Task<LdapConnection> CreateLdapConnection(Tenant tenant)
        {
            var ldapConnection = new LdapConnection();
            ldapConnection.Connect(await _settings.GetDomain(tenant?.Id), await _settings.GetPort(tenant?.Id));
            ldapConnection.Bind(await _settings.GetUserName(tenant?.Id), await _settings.GetPassword(tenant?.Id));
            return ldapConnection;
        }

        protected virtual async Task<string> CreateSearchFilter(string userNameOrEmailAddress, Tenant tenant)
        {
            var mailSuffix = ConvertToNullIfEmpty(await _settings.GetMailSuffix(tenant?.Id));
            var sAMAccountName = userNameOrEmailAddress;

            if (userNameOrEmailAddress.Contains(mailSuffix))
                sAMAccountName = userNameOrEmailAddress.Substring(0, userNameOrEmailAddress.IndexOf(mailSuffix));

            return $"(sAMAccountName={sAMAccountName})";
        }

        protected virtual async Task CheckIsEnabled(Tenant tenant)
        {
            var tenantId = tenant?.Id;
            if (!await _settings.GetIsEnabled(tenantId))
            {
                throw new AbpException("Ldap Authentication is disabled for given tenant (id:" + tenantId + ")!");
            }
        }

        protected static string ConvertToNullIfEmpty(string str)
        {
            return str.IsNullOrWhiteSpace()
                ? null
                : str;
        }
    }
}
