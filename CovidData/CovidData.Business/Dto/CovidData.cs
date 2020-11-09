using System;

namespace Covid.Business.Dto
{
    public class CovidData
    {
        public int QuestionId { get; set; }
        public int Category { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public DateTime Datetime { get; set; }
    }
}