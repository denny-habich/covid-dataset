using Covid.Data.Entities;
using MongoDB.Driver;

namespace Covid.Data.Repositories
{
    public abstract class BaseCollection<T> where T : IDataCollection
    {
        protected readonly MongoCollectionBase<T> Collection;

        public BaseCollection(ICovidDatabaseSettings settings, string collectionName)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            
            Collection = (MongoCollectionBase<T>)database.GetCollection<T>(collectionName);
        }
    }
}