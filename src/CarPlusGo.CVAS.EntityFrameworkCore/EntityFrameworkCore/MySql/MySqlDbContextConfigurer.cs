using Abp.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace CarPlusGo.CVAS.EntityFrameworkCore
{
    public static class MySqlDbContextConfigurer
    {
        public static void Configure<T>(DbContextOptionsBuilder<T> builder, string connectionString) where T : AbpDbContext
        {
            builder.UseMySql(connectionString);
        }

        public static void Configure<T>(DbContextOptionsBuilder<T> builder, DbConnection connection) where T : AbpDbContext
        {
            builder.UseMySql(connection);
        }
    }
}
