using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvvmNano;
using TutorScout24.Services;

namespace TutorScout24.Models
{
    public class TutoringRequest
    {
        public int latitude { get; set; }
        public int longitude { get; set; }
        public int rangeKm { get; set; }
        public int rowLimit { get; set; }
        public int rowOffset { get; set; }
        public Authentication authentication { get; set; }

    }
}
