namespace TutorScout24.Models.Tutorings
{
    public class TutoringRequest : RestCommandWithAuthentication
    {
        public int latitude { get; set; }
        public int longitude { get; set; }
        public int rangeKm { get; set; }
        public int rowLimit { get; set; }
        public int rowOffset { get; set; }
    }
}