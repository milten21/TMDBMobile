using TMDBMobile.Core.Model;
using TMDBMobile.Core.PageModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TMDBMobile.Core.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FavoriteMoviesPage : ContentPage
    {
        public FavoriteMoviesPageModel PageModel => BindingContext as FavoriteMoviesPageModel;

		public FavoriteMoviesPage ()
		{
			InitializeComponent ();
		}

        private void MovieItemAppearing(object sender, ItemVisibilityEventArgs e)
        {
            if (PageModel.Movies.Count == 0)
                return;

            if ((Movie)e.Item == PageModel.Movies[PageModel.Movies.Count - 1])
                PageModel.LoadFavoritePageCommand.Execute(null);
        }

        private void HandleItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (!(sender is ListView listView))
                return;

            listView.SelectedItem = null;
        }
	}
}