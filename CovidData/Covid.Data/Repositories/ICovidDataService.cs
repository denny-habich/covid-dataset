using System.Collections.Generic;
using Covid.Data.Entities;
using MongoDB.Bson;

namespace Covid.Data.Repositories
{
    public interface ICovidDataService<IDataCollection>
    {
        public List<IDataCollection> Get();

        public IDataCollection Get(string id);

        public IDataCollection InsertOne(IDataCollection data);

        public void InsertMany(IEnumerable<IDataCollection> data);

        public void Truncate();

        public void Aggregate();

        public void Update(string id, IDataCollection dataIn);

        public void Remove(IDataCollection dataIn);

        public void Remove(string id);
    }
}