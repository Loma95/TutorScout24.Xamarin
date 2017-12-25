using System;
namespace TutorScout24.Utils
{
    public class DateTimeUtils
    {
        public DateTimeUtils()
        {
        }

        /// <summary>
        /// calculates the age from the given datetime
        /// </summary>
        /// <param name="dateOfBirth"></param>
        /// <returns></returns>
        public static int CalculateAge(DateTime dateOfBirth)
        {
            int age = 0;
            age = DateTime.Now.Year - dateOfBirth.Year;
            if (DateTime.Now.DayOfYear < dateOfBirth.DayOfYear)
                age = age - 1;

            return age;
        }
    }
}
