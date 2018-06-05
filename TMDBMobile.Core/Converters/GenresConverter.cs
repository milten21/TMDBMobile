using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using FreshMvvm;
using Xamarin.Forms;

namespace TMDBMobile.Core.Converters
{
    public class GenresConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is List<int> genreIds))
                return string.Empty;

            var genres = FreshIOC.Container.Resolve<IAppStoreContainer>()
                                 .Store.GetState().DataState.Genres;

            if (genres == null)
                return string.Empty;

            string result = string.Empty;

            foreach(var genreId in genreIds)
            {
                var genre = genres.FirstOrDefault(g => g.Id == genreId);

                if (genre == null)
                    continue;

                result += genre.Name + ", ";
            }

            if (string.IsNullOrEmpty(result))
                return result;

            return result.Substring(0, result.Length - 2);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
