using Abp.Domain.Repositories;
using MongoDB.Driver;

namespace CarPlusGo.CVAS.Mobile.MongoDB
{
    public interface IMongoDBRepository : IRepository
    {
        IMongoDatabase Database { get; }
    }
}
