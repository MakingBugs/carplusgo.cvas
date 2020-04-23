using System.Threading.Tasks;

namespace CarPlusGo.CVAS.Authorization.Ldap.Configuration
{
    /// <summary>
    /// Used to obtain current values of LDAP settings.
    /// This abstraction allows to define a different source for settings than SettingManager.
    /// </summary>
    public interface ILdapSettings
    {
        Task<bool> GetIsEnabled(int? tenantId);

        Task<string> GetContainer(int? tenantId);

        Task<string> GetDomain(int? tenantId);

        Task<int> GetPort(int? tenantId);

        Task<string> GetMailSuffix(int? tenantId);

        Task<string> GetUserName(int? tenantId);

        Task<string> GetPassword(int? tenantId);
    }
}
