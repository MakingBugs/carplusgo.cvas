using Abp.MultiTenancy;
using CarPlusGo.CVAS.Authorization.Users;

namespace CarPlusGo.CVAS.MultiTenancy
{
    public class Tenant : AbpTenant<User>
    {
        public Tenant()
        {            
        }

        public Tenant(string tenancyName, string name)
            : base(tenancyName, name)
        {
        }
    }
}
