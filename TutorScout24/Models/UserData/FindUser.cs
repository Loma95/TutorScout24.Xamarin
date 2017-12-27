namespace TutorScout24.Models.UserData
{
    public class FindUser : RestCommandWithAuthentication
    {
        public string userToFind { get; set; }
    }
}