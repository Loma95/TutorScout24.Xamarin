using System;
using Xamarin.Forms;

namespace TutorScout24.Utils
{
    public class ChatDateConverter : IValueConverter
    {
        

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
                return string.Empty;

            var datetime = (DateTime)value;
            long span = DateTime.Now.Ticks - datetime.Ticks;
            TimeSpan elapsedSpan = new TimeSpan(span);
            string display = "";
            if(elapsedSpan.Days == 0){
                display = "Heute um " + datetime.Hour + ":" + datetime.Minute;
            }else if(elapsedSpan.Days == 1){
                display = "Gestern um " + datetime.Hour + ":" + datetime.Minute;
            }else{
                display = "Am " + datetime.ToString("dd.MM.yyyy");
            }

            return display;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

     
    }
}
