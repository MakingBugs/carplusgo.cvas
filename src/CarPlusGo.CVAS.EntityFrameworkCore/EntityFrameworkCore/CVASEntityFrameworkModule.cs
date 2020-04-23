using Abp.Configuration.Startup;
using Abp.Domain.Uow;
using Abp.EntityFrameworkCore;
using Abp.EntityFrameworkCore.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Abp.Zero.EntityFrameworkCore;
using CarPlusGo.CVAS.EntityFrameworkCore.Seed;

namespace CarPlusGo.CVAS.EntityFrameworkCore
{
    [DependsOn(
        typeof(CVASCoreModule), 
        typeof(AbpZeroCoreEntityFrameworkCoreModule))]
    public class CVASEntityFrameworkModule : AbpModule
    {
        /* Used it tests to skip dbcontext registration, in order to use in-memory database of EF Core */
        public bool SkipDbContextRegistration { get; set; }

        public bool SkipDbSeed { get; set; }

        public override void PreInitialize()
        {
            if (!SkipDbContextRegistration)
            {
                Configuration.ReplaceService<IConnectionStringResolver, CVASConnectionStringResolver>();
                //添加所有数据库上下文
                this.AddSqlServerDbContext<CVASDbContext>();
                this.AddSqlServerDbContext<TShareBankDbContext>();
                this.AddSqlServerDbContext<RentalDbContext>();
                this.AddMySqlDbContext<BiStatDbContext>();
            }
        }

        private void AddSqlServerDbContext<TDbContext>()
            where TDbContext : AbpDbContext
        {
            Configuration.Modules.AbpEfCore().AddDbContext<TDbContext>(options =>
            {
                if (options.ExistingConnection != null)
                {
                    SqlServerDbContextConfigurer.Configure(options.DbContextOptions, options.ExistingConnection);
                }
                else
                {
                    SqlServerDbContextConfigurer.Configure(options.DbContextOptions, options.ConnectionString);
                }
            });
        }

        private void AddMySqlDbContext<TDbContext>()
            where TDbContext : AbpDbContext
        {
            Configuration.Modules.AbpEfCore().AddDbContext<TDbContext>(options =>
            {
                if (options.ExistingConnection != null)
                {
                    MySqlDbContextConfigurer.Configure(options.DbContextOptions, options.ExistingConnection);
                }
                else
                {
                    MySqlDbContextConfigurer.Configure(options.DbContextOptions, options.ConnectionString);
                }
            });
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(CVASEntityFrameworkModule).GetAssembly());
        }

        public override void PostInitialize()
        {
            if (!SkipDbSeed)
            {
                SeedHelper.SeedHostDb(IocManager);
            }
        }
    }
}
