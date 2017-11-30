using System;
namespace TutorScout24.Models
{
   

        public class UpdateUser
        {
            
            public string firstName { get; set; }
            public string lastName { get; set; }
            public int age { get; set; }
            public string gender { get; set; }
            public string note { get; set; }
         
            public Authentication authentication { get; set; }
        }

}
