using System;
using System.Globalization;
using TMDBMobile.Core.Model;
using Xamarin.Forms;

namespace TMDBMobile.Core.Converters
{
    public class ProfileTitleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is Profile profile))
                return string.Empty;

            return $"Hello, {profile.Name ?? profile.Username}";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
