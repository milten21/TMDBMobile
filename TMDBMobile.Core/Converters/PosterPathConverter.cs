using FreshMvvm;
using System;
using System.Globalization;
using TMDBMobile.Core.Services;
using Xamarin.Forms;

namespace TMDBMobile.Core.Converters
{
    public class PosterPathConverter : IValueConverter
    {
        private static string _imagesPath;
        private static string ImagesPath => _imagesPath ?? 
            (_imagesPath = FreshIOC.Container.Resolve<IConfigurationService>().ImagesPath);

        public static string Convert(string path)
        {
            return ImagesPath + path; 
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is string path))
                return string.Empty;

            return Convert(path);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
