using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Covid.Data.Entities
{
    public class ImportStatistics : IDataCollection
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public DateTime StartDateTime { get; set; }

        public DateTime EndDateTime { get; set; }

        public int ImportedFiles { get; set; }

        public int LinesImported { get; set; }
        public int TotalFiles { get; set; }
    }
}
