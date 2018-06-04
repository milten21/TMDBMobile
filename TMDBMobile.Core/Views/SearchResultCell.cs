using FFImageLoading.Forms;
using TMDBMobile.Core.Converters;
using TMDBMobile.Core.Model;
using Xamarin.Forms;

namespace TMDBMobile.Core.Views
{
    public class SearchResultCell : ViewCell
    {

        protected override void OnBindingContextChanged()
        {
            var poster = View.FindByName<CachedImage>("poster");

            // prevent showing old image
            poster.Source = null;

            var item = BindingContext as Movie;

            if (item == null)
                return;

            // avoid using data binding
            poster.Source = PosterPathConverter.Convert(item.PosterPath);

            base.OnBindingContextChanged();
        }
    }
}
