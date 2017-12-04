using System;
namespace TutorScout24.Models
{
    public class UpdateProfile: RestCommandWithAuthentication
    {
       
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string gender { get; set; }
        public string note { get; set; }
        public string maxGraduation { get; set; }
  
        }

}
