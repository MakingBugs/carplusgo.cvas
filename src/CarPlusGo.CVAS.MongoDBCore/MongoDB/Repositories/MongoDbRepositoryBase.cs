using CarPlusGo.CVAS.Mobile.MongoDB;
using MongoDB.Driver;

namespace CarPlusGo.CVAS.MongoDBCore.MongoDB.Repositories
{
    /// <summary>
    /// Implements IRepository for MongoDB.
    /// </summary>
    public class MongoDbRepositoryBase : IMongoDBRepository
    {
        public virtual IMongoDatabase Database
        {
            get { return _databaseProvider.Database; }
        }

        private readonly IMongoDatabaseProvider _databaseProvider;

        public MongoDbRepositoryBase(IMongoDatabaseProvider databaseProvider)
        {
            _databaseProvider = databaseProvider;
        }
    }
}
