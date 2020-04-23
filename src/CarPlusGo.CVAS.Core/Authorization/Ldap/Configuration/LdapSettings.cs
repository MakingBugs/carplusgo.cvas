using Abp.Dependency;
using Novell.Directory.Ldap;
using System.Threading.Tasks;

namespace CarPlusGo.CVAS.Authorization.Ldap.Configuration
{
    public class LdapSettings : ILdapSettings, ITransientDependency
    {
        private const string DomainName = "192.168.21.1";
        private const string Container = "DC=carplusgo,DC=com";
        private const string MailSuffix = "@carplusgo.com";
        private const string UserName = "dawei.yu@carplusgo.com";
        private const string Password = "qwe123!";
        private const bool IsEnabled = true;

        public virtual Task<bool> GetIsEnabled(int? tenantId)
        {
            return Task.FromResult(tenantId.HasValue
                ? IsEnabled
                : IsEnabled);
        }

        public virtual Task<string> GetContainer(int? tenantId)
        {
            return Task.FromResult(tenantId.HasValue
                ? Container
                : Container);
        }

        public virtual Task<string> GetDomain(int? tenantId)
        {
            return Task.FromResult(tenantId.HasValue
                ? DomainName
                : DomainName);
        }

        public virtual Task<int> GetPort(int? tenantId)
        {
            return Task.FromResult(tenantId.HasValue
                ? LdapConnection.DEFAULT_PORT
                : LdapConnection.DEFAULT_PORT);
        }

        public virtual Task<string> GetMailSuffix(int? tenantId)
        {
            return Task.FromResult(tenantId.HasValue
                ? MailSuffix
                : MailSuffix);
        }

        public virtual Task<string> GetUserName(int? tenantId)
        {
            return Task.FromResult(tenantId.HasValue
                ? UserName
                : UserName);
        }

        public virtual Task<string> GetPassword(int? tenantId)
        {
            return Task.FromResult(tenantId.HasValue
                ? Password
                : Password);
        }
    }
}
