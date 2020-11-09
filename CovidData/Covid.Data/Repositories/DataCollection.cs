using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Covid.Data.Entities;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Covid.Data.Repositories
{
    public class DataCollection<T> : BaseCollection<T> where T : IDataCollection 
    {
        public DataCollection(ICovidDatabaseSettings settings, string collectionName) : base(settings, collectionName) { }
        
        public List<T> Get() => Collection.Find(data => true).ToList();

        public T Get(string id) => Collection.Find(data => data.Id == id).FirstOrDefault();

        public T InsertOne(T data)
        {
            Collection.InsertOne(data);
            return data;
        }

        public void InsertMany(IEnumerable<T> data) => Collection.InsertMany(data);

        public Task InsertManyAsync(IEnumerable<T> data)
        {
            return Collection.InsertManyAsync(data);
        }

        public void Truncate() => Collection.Database.DropCollection(Collection.CollectionNamespace.CollectionName);

        public void Aggregate(PipelineDefinition<T, BsonDocument> pipeline) => Collection.AggregateToCollection(pipeline);

        public void Update(string id, T dataIn) => Collection.ReplaceOne(data => data.Id == id, dataIn);

        public void FindOneAndReplace(FilterDefinition<T> expression, T dataIn)
        {
            Collection.FindOneAndReplace(expression, dataIn);
        }

        public void Remove(T dataIn) => Collection.DeleteOne(data => data.Id == dataIn.Id);

        public void Remove(string id) => Collection.DeleteOne(data => data.Id == id);
    }
}
