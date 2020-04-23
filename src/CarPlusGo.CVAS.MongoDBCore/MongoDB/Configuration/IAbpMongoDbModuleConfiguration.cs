namespace CarPlusGo.CVAS.MongoDBCore.MongoDB.Configuration
{
    public interface IAbpMongoDbModuleConfiguration
    {
        string ConnectionString { get; set; }

        string DatabaseName { get; set; }
    }
}
