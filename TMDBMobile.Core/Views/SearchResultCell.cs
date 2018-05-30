using FFImageLoading.Forms;
using System;
using System.Collections.Generic;
using System.Text;
using TMDBMobile.Core.Converters;
using TMDBMobile.Core.Model;
using Xamarin.Forms;

namespace TMDBMobile.Core.Views
{
    public class SearchResultCell : ViewCell
    {
        readonly CachedImage _cachedImage = null;

        public SearchResultCell()
        {
            _cachedImage = new CachedImage();
        }

        protected override void OnBindingContextChanged()
        {
            // you can also put cachedImage.Source = null; here to prevent showing old images occasionally
            _cachedImage.Source = null;


            // unboxing... =_=
            var item = BindingContext as Movie;

            if (item == null)
                return;
            
            _cachedImage.Source = PosterPathConverter.Convert(item.PosterPath);

            base.OnBindingContextChanged();
        }
    }
}
