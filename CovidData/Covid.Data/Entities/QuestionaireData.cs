using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Covid.Data.Entities
{
    public class QuestionaireData : IDataCollection
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public DateTime ImportDate { get; set; }

        public DateTime Datetime { get; set; }

        public string Answer { get; set; }
        public int Category { get; set; }
        public string Question { get; set; }
        public int QuestionId { get; set; }
    }
}
