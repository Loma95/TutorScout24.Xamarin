using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TutorScout24.Models
{
    public class Tutoring
    {
        public int tutoringId { get; set; }
        public DateTime creationDate { get; set; }
        public string userName { get; set; }
        public string subject { get; set; }
        public string text { get; set; }
        public DateTime expirationDate { get; set; }
        public int latitude { get; set; }
        public int longitude { get; set; }
        public double distanceKm { get; set; }
    }
}
