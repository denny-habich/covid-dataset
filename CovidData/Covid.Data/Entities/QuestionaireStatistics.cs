using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Covid.Data.Entities
{
    public class QuestionaireStatistics : IDataCollection
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public int Count { get; set; }

        public int Type { get; set; }

        public int IntValue { get; set; }
        public string TextValue { get; set; }
    }

    public enum StatisticType
    {
        Question,
        Answer,
        Category
    }
}
