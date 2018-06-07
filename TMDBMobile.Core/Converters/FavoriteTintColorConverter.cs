using System;
using System.Globalization;
using Xamarin.Forms;

namespace TMDBMobile.Core.Converters
{
    public class FavoriteTintColorConverter : IValueConverter
    {
        public Color NotFavoriteColor => Color.Silver;

        public Color FavoriteColor => (OnPlatform<Color>)Application.Current.Resources["PrimaryColor"];

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is bool isFavorite))
                return NotFavoriteColor;
            
            return isFavorite ? FavoriteColor : NotFavoriteColor;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
