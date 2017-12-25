using System;
using System.Collections.Generic;
using System.Linq;
using TutorScout24.Models.Chat;
using Xamarin.Forms;

namespace TutorScout24.Utils
{
    class LastItemConverter : IValueConverter
    {
        private const int MAXTEXTLENGTH = 100;

        /// <summary>
        /// gets the last message from the conversation
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        object IValueConverter.Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            IEnumerable<Message> items = value as IEnumerable<Message>;
            if (items != null)
            {
                   Message m = (Message)items.LastOrDefault();
                if (m != null)
                {
                    if(m.Text.Length < MAXTEXTLENGTH)
                    {
                        return m.Text;
                    }
                    else
                    {
                        return m.Text.Substring(0, MAXTEXTLENGTH) + "...";
                    }
                
                }
                  
                else
                    return "";

            }
            else return "";
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new System.NotImplementedException();
        }
    }
}
