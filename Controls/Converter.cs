using System;
using System.Collections;
using System.Globalization;

namespace FireSignage
{
   

    public class Converter
    {



    }


    #region BoolToImageConverter

    public class BoolToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? "group_expand.png" : "group_collapse.png";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    #endregion


}