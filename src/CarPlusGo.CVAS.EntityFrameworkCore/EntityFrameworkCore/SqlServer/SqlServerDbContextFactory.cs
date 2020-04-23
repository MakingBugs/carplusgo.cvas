using Abp.EntityFrameworkCore;
using CarPlusGo.CVAS.Configuration;
using CarPlusGo.CVAS.Web;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;

namespace CarPlusGo.CVAS.EntityFrameworkCore
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public abstract class SqlServerDbContextFactory<T> : IDesignTimeDbContextFactory<T>
        where T : AbpDbContext
    {
        /// <summary>
        /// 数据库连接节点名称
        /// </summary>
        public abstract string ConnectionStringName { get; }
        /// <summary>
        /// 用反射创建DbContext实例
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public T CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<T>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());
            SqlServerDbContextConfigurer.Configure(
                builder,
                configuration.GetConnectionString(ConnectionStringName)
            );
            return (T)Activator.CreateInstance(typeof(T), new object[] { builder.Options });
        }
    }
}
