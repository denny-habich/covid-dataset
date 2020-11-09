namespace Covid.Business.Configuration
{
    public class ImportOptions
    {
        public string Source { get; set; }
        public string FilePattern { get; set; }
        public char FieldsSeparator { get; set; }
        public int ImportIntervalSeconds { get; set; }
    }
}