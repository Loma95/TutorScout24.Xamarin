using System;
namespace TutorScout24.Models
{
    public class FindUser: RestCommandWithAuthentication
    {
        public string userToFind { get; set; }
    }
}
