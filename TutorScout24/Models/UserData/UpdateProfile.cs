namespace TutorScout24.Models.UserData
{
    public class UpdateProfile : RestCommandWithAuthentication
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string gender { get; set; }
        public string note { get; set; }
        public string maxGraduation { get; set; }
        public string email { get; set; }
    }
}