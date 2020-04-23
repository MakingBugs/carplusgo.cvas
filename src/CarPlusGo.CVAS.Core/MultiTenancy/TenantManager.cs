using Abp.Application.Features;
using Abp.Domain.Repositories;
using Abp.MultiTenancy;
using CarPlusGo.CVAS.Authorization.Users;
using CarPlusGo.CVAS.Editions;

namespace CarPlusGo.CVAS.MultiTenancy
{
    public class TenantManager : AbpTenantManager<Tenant, User>
    {
        public TenantManager(
            IRepository<Tenant> tenantRepository, 
            IRepository<TenantFeatureSetting, long> tenantFeatureRepository, 
            EditionManager editionManager,
            IAbpZeroFeatureValueStore featureValueStore) 
            : base(
                tenantRepository, 
                tenantFeatureRepository, 
                editionManager,
                featureValueStore)
        {
        }
    }
}
