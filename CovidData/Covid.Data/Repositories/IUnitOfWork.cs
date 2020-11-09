using System;
using System.Collections.Generic;
using System.Text;
using Covid.Data.Entities;

namespace Covid.Data.Repositories
{
    public interface IUnitOfWork
    {
        DataCollection<T> GetCollection<T>() where T : IDataCollection;

    }
}
