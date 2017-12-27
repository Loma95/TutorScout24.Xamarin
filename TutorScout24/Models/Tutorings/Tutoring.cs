using System;

namespace TutorScout24.Models.Tutorings
{
    public class Tutoring
    {
        public int tutoringId { get; set; }
        public DateTime creationDate { get; set; }
        public string userName { get; set; }
        public string subject { get; set; }
        public string text { get; set; }
        public DateTime expirationDate { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public double distanceKm { get; set; }
        public int daysLeft { get; set; }
    }
}