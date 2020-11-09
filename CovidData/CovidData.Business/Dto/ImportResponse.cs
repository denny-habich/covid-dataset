using System;

namespace Covid.Business.Dto
{
    public class ImportResponse
    {
        public DateTime StartDateTime { get; set; }

        public DateTime EndDateTime { get; set; }

        public int ImportedFiles { get; set; }

        public int LinesImported { get; set; }
        public int TotalFiles { get; set; }
    }
}
