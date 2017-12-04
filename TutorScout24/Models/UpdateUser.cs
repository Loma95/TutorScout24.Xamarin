﻿using System;
namespace TutorScout24.Models
{
   

    public class UpdateUser: RestCommandWithAuthentication
        {
            public string password { get; set; }
            public string firstName { get; set; }
            public string lastName { get; set; }
            public string birthdate { get; set; }
            public string gender { get; set; }
            public string email { get; set; }
            public string note { get; set; }
            public string placeOfResidence { get; set; }
            public string maxGraduation { get; set; }
        }

}
