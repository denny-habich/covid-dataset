using System.Collections.Generic;

namespace Covid.Data.Entities
{
    public class Question
    {
        public int Id { get; set; }

        //public int ExternalId { get; set; }

        public Category Category { get; set; }

        public string Text { get; set; }

        public IEnumerable<Answer> Answers { get; set; }
    }
}
