using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using CarPlusGo.CVAS.Authorization.Roles;
using CarPlusGo.CVAS.Authorization.Users;
using CarPlusGo.CVAS.MultiTenancy;

namespace CarPlusGo.CVAS.EntityFrameworkCore
{
    public class CVASDbContext : AbpZeroDbContext<Tenant, Role, User, CVASDbContext>
    {
        /* Define a DbSet for each entity of the application */
        public CVASDbContext(DbContextOptions<CVASDbContext> options)
            : base(options)
        {
        }
    }
}
