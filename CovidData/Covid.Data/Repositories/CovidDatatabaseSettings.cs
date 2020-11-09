using System;
using System.Collections.Generic;
using System.Text;

namespace Covid.Data.Repositories
{
    public class CovidDatabaseSettings : ICovidDatabaseSettings
    {
        public string CovidDataCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface ICovidDatabaseSettings
    {
        string CovidDataCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}