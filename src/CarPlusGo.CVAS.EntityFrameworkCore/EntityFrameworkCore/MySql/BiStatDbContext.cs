using Abp.EntityFrameworkCore;
using CarPlusGo.CVAS.Mobile.BiStat;
using Microsoft.EntityFrameworkCore;

namespace CarPlusGo.CVAS.EntityFrameworkCore
{
    public class BiStatDbContext : AbpDbContext
    {
        /* Define a DbSet for each entity of the application */
        public DbSet<DayDriverTime> DayDriverTime { set; get; }
        public BiStatDbContext(DbContextOptions<BiStatDbContext> options)
            : base(options)
        {
        }
    }
}
