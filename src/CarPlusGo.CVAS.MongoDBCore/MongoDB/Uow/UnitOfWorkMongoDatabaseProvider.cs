using Abp.Dependency;
using CarPlusGo.CVAS.Configuration;
using CarPlusGo.CVAS.Web;
using MongoDB.Driver;

namespace CarPlusGo.CVAS.MongoDBCore.MongoDB.Uow
{
    /// <summary>
    /// Implements <see cref="IMongoDatabaseProvider"/> that gets database from active unit of work.
    /// </summary>
    public class UnitOfWorkMongoDatabaseProvider : IMongoDatabaseProvider, ITransientDependency
    {
        public IMongoDatabase Database { get; set; }

        public UnitOfWorkMongoDatabaseProvider()
        {
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());
            Database = new MongoClient(configuration[CVASConsts.ShareConnectionStringName + ":ConnectionString"]).GetDatabase(configuration[CVASConsts.ShareConnectionStringName + ":DatabaseName"]);
        }
    }
}
