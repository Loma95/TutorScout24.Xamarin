using System;
using Xamarin.Forms;

namespace TutorScout24.Utils
{
    public class ChatDateConverter : IValueConverter
    {
        
        /// <summary>
        /// Converts the time to a readable format to display it in the chats
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
                return string.Empty;

            var datetime = (DateTime)value;
            long span = DateTime.Now.Ticks - datetime.Ticks;
            TimeSpan elapsedSpan = new TimeSpan(span);
            string display = "";
            if(elapsedSpan.Days == 0){
                display = "Heute um " + datetime.Hour.ToString("D2") + ":" + datetime.Minute.ToString("D2");
            }else if(elapsedSpan.Days == 1){
                display = "Gestern um " + datetime.Hour.ToString("D2") + ":" + datetime.Minute.ToString("D2");
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
