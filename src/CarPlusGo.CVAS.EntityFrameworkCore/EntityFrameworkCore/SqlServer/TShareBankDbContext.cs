using Abp.EntityFrameworkCore;
using CarPlusGo.CVAS.Mobile.TShareBank;
using Microsoft.EntityFrameworkCore;

namespace CarPlusGo.CVAS.EntityFrameworkCore
{
    public class TShareBankDbContext:AbpDbContext
    {
        /* Define a DbSet for each entity of the application */
        public DbSet<TargetConfig> TargetConfig { set; get; }
        public DbSet<UmengApiData> UmengApiData { set; get; }
        public DbSet<OperationTarget> OperationTarget { set; get; }
        public TShareBankDbContext(DbContextOptions<TShareBankDbContext> options)
            : base(options)
        {
        }
    }
}
