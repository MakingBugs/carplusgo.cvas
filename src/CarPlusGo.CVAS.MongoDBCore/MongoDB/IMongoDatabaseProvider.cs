using MongoDB.Driver;

namespace CarPlusGo.CVAS.MongoDBCore.MongoDB
{
    /// <summary>
    /// Defines interface to obtain a <see cref="MongoDatabase"/> object.
    /// </summary>
    public interface IMongoDatabaseProvider
    {
        /// <summary>
        /// Gets the <see cref="IMongoDatabase"/>.
        /// </summary>
        IMongoDatabase Database { get; }
    }
}