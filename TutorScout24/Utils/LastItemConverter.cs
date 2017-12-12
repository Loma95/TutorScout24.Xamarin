using System;
using System.Collections.Generic;
using System.Linq;
using TutorScout24.Models.Chat;
using Xamarin.Forms;

namespace TutorScout24.Utils
{
    class LastItemConverter : IValueConverter
    {
        object IValueConverter.Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            IEnumerable<Message> items = value as IEnumerable<Message>;
            if (items != null)
            {
                Message m = (Message)items.LastOrDefault();
                return m.Text;
            }
            else return "";
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new System.NotImplementedException();
        }
    }
}
