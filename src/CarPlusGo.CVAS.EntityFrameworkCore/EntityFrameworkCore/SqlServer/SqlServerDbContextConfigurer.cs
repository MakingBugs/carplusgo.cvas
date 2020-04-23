using System.Data.Common;
using Abp.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CarPlusGo.CVAS.EntityFrameworkCore
{
    public static class SqlServerDbContextConfigurer
    {
        public static void Configure<T>(DbContextOptionsBuilder<T> builder, string connectionString) where T : AbpDbContext
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure<T>(DbContextOptionsBuilder<T> builder, DbConnection connection) where T : AbpDbContext
        {
            builder.UseSqlServer(connection);
        }
    }
}
