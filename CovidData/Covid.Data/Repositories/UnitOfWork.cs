using System;
using System.Collections.Generic;
using System.Text;
using Covid.Data.Entities;

namespace Covid.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private Dictionary<Type, object> _collections;

        public ICovidDatabaseSettings Settings { get; }

        public UnitOfWork(ICovidDatabaseSettings settings)
        {
            Settings = settings ?? throw new ArgumentNullException(nameof(settings));
        }

        public DataCollection<T> GetCollection<T>() where T : IDataCollection
        {
            _collections ??= new Dictionary<Type, object>();

            var type = typeof(T);
            if (!_collections.ContainsKey(type)) _collections[type] = new DataCollection<T>(Settings, type.Name);
            return (DataCollection<T>)_collections[type];
        }
        
        //public void Dispose()
        //{
        //    Context?.Dispose();
        //}
    }
}
