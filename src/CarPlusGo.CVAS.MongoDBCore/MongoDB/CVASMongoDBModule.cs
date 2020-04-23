using Abp;
using Abp.Modules;
using CarPlusGo.CVAS.MongoDBCore.Configuration.Startup;
using CarPlusGo.CVAS.MongoDBCore.MongoDB.Configuration;
using Abp.Reflection.Extensions;
using CarPlusGo.CVAS.Configuration;
using CarPlusGo.CVAS.Web;

namespace CarPlusGo.CVAS.MongoDBCore.MongoDB
{
    [DependsOn(typeof(AbpKernelModule))]
    public class CVASMongoDBModule : AbpModule
    {
        public override void PreInitialize()
        {
            IocManager.Register<IAbpMongoDbModuleConfiguration, AbpMongoDbModuleConfiguration>();

            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());
            //MongoDB数据库地址
            Configuration.Modules.AbpMongoDb().ConnectionString = configuration[CVASConsts.ShareConnectionStringName+ ":ConnectionString"];
            //MongoDB数据库名称
            Configuration.Modules.AbpMongoDb().DatabaseName = configuration[CVASConsts.ShareConnectionStringName + ":DatabaseName"];
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(CVASMongoDBModule).GetAssembly());
        }
    }
}
