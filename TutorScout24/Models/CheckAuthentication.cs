using System;
namespace TutorScout24.Models
{
    public class Authentication
    {
        public string userName { get; set; }
        public string password { get; set; }
    }

    public class CheckAuthentication
    {
        public Authentication authentication { get; set; }
    }
}
