using TMDBMobile.Core.Model;
using TMDBMobile.Core.PageModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TMDBMobile.Core.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SearchPage : ContentPage
	{
        public SearchPageModel PageModel => BindingContext as SearchPageModel;

		public SearchPage ()
		{
			InitializeComponent ();
		}

        private void SearchItemAppearing(object sender, ItemVisibilityEventArgs e)
        {
            if (PageModel.Movies.Count == 0)
                return;

            if ((Movie)e.Item == PageModel.Movies[PageModel.Movies.Count - 1])
                PageModel.LoadNextPageCommand.Execute(null);
        }

        private void DiscoverItemAppearing(object sender, ItemVisibilityEventArgs e)
        {
            if (PageModel.DiscoverMovies.Count == 0)
                return;

            if ((Movie)e.Item == PageModel.DiscoverMovies[PageModel.DiscoverMovies.Count - 1])
                PageModel.LoadDiscoverPageCommand.Execute(null);
        }
    }
}