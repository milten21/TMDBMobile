using System;
using System.Globalization;
using Xamarin.Forms;

namespace TMDBMobile.Core.Converters
{
    public class LikeColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is int rate) || rate < 5 || rate == 0)
                return Color.Silver;

            return Color.Green;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
