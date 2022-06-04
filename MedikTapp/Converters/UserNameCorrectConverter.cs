using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace MedikTapp.Converters
{
    public class UserNameCorrectConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return isUserNameCorrect(value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private bool isUserNameCorrect(object value)
        {
            if (value is string)
            {
                bool isEmail = Regex.IsMatch(
                    (string)value, "^[a-z0-9._-]+@[a-z0-9._-]+\\.[a-z]{2,6}$");

                int length = ((string)value).Trim().Length;

                if (length >= 7 && length <= 60 && isEmail)
                    return true;
                else
                    return false;

            }
            return false;
        }

    }


}
