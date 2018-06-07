﻿using System;
using System.Globalization;
using Xamarin.Forms;

namespace TMDBMobile.Core.Converters
{
    public class FavoriteIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is bool isFavorite))
                return "starEmpty";

            return isFavorite ? "starFilled" : "starEmpty";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
