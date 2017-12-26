using System;
using TutorScout24.Models;

namespace TutorScout24.Utils
{
    public class SelectedLocationArgs:EventArgs
    {
        public SelectedLocationArgs(double la,double lo)
        {
            Lat = la;
            Lon = lo;
        }

        public double Lat
        {
            get;
            set;
        }

        public double Lon
        {
            get;
            set;
        }
    }
}
