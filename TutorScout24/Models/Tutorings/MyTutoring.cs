using System;

namespace TutorScout24.Models.Tutorings
{
    public class MyTutoring
    {
        public int tutoringid { get; set; }
        public DateTime creationdate { get; set; }
        public string createruserid { get; set; }
        public string subject { get; set; }
        public string text { get; set; }
        public string type { get; set; }
        public DateTime end { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
    }
}