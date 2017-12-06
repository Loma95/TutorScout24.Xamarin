using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TutorScout24.Models
{
    public class CreateTutoring: RestCommandWithAuthentication
    {
        public string subject { get; set; }
        public string text { get; set; }
        public int duration { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
    }
}
