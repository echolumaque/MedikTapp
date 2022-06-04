using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace MedikTapp.Converters
{
    public class PasswordCorrectConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return isPasswordCorrect(value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private bool isPasswordCorrect(object value)
        {
            if (value is string)
            {
                int length = ((string)value).Trim().Length;
                if (length >= 8)
                    return true;
                else
                    return false;
            }
            return false;
        }
    }
}
