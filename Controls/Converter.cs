using System;
using System.Collections;
using System.Globalization;

namespace FireSignage
{
   

    public class Converter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int result = 0;
            var items = value as IEnumerable;
            if (items != null)
            {
                var items = items.ToList<object>().ToList<object>();
                if (items != null)
                {
                    for (int i = 0; i < items.Count; i++)
                    {
                        var contact = items[i] as Contacts;
                        result += contact.ContactNumber;
                    }
                }
            }
            return result
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }



}