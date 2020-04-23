using Abp.Configuration.Startup;
using Abp.Domain.Uow;
using CarPlusGo.CVAS.Configuration;
using CarPlusGo.CVAS.Web;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;

namespace CarPlusGo.CVAS.EntityFrameworkCore
{
    public class CVASConnectionStringResolver : DefaultConnectionStringResolver
    {
        private Dictionary<Type, string> dbConnectionStrings = new Dictionary<Type, string>();

        private readonly IHostingEnvironment _env;
        public CVASConnectionStringResolver(IAbpStartupConfiguration configuration, IHostingEnvironment env)
            : base(configuration)
        {
            _env = env;
            //添加所有数据库连接节点名称
            dbConnectionStrings.Add(typeof(CVASDbContext), CVASConsts.ConnectionStringName);
            dbConnectionStrings.Add(typeof(TShareBankDbContext), CVASConsts.TShareBankConnectionStringName);
            dbConnectionStrings.Add(typeof(RentalDbContext), CVASConsts.RentalConnectionStringName);
            dbConnectionStrings.Add(typeof(BiStatDbContext), CVASConsts.BiStatConnectionStringName);
        }

        public override string GetNameOrConnectionString(ConnectionStringResolveArgs args)
        {
            var connectStringName = this.GetConnectionStringName(args);
            if (connectStringName != null)
            {
                var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder(), _env.EnvironmentName, _env.IsDevelopment());
                return configuration.GetConnectionString(connectStringName);
            }
            return base.GetNameOrConnectionString(args);
        }
        private string GetConnectionStringName(ConnectionStringResolveArgs args)
        {
            var type = args["DbContextConcreteType"] as Type;
            if (dbConnectionStrings.ContainsKey(type))
            {
                return dbConnectionStrings[type];
            }
            return null;
        }
    }
}
