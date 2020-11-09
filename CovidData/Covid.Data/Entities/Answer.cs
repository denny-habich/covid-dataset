namespace Covid.Data.Entities
{
    public class Answer
    {
        public int Id { get; set; }

        public Question Question { get; set; }

        public string Text { get; set; }
    }
}
