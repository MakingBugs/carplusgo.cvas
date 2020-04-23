using CarPlusGo.CVAS.Mobile.MongoDB;
using MongoDB.Bson;

namespace CarPlusGo.CVAS.MongoDBCore.MongoDB.Repositories
{
    public class MongoDBRepository : MongoDbRepositoryBase, IMongoDBRepository
    {
        public MongoDBRepository(IMongoDatabaseProvider databaseProvider)
            : base(databaseProvider)
        {
        }
    }
}
