using System;
using System.Text.RegularExpressions;

namespace TutorScout24.Utils
{
    public static class InputValidator
    {

        public static bool IsValidEmail(string mail){
            
            Regex r = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            return IsNotEmpty(mail) && r.IsMatch(mail);
        }

        public static bool IsNotEmpty(string text){
            return text != null && text.Length > 0;
        }

        public static bool IsValidPassword(string pass){
            Regex r = new Regex(@"((?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[@#$%]).{8,70})$");
 
        
            return IsNotEmpty(pass) && r.IsMatch(pass);
        }
    }
}
