using System;
using TMDBMobile.Core.Model;
using TMDBMobile.Core.PageModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TMDBMobile.Core.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ReviewsPage : ContentPage
    {
        public ReviewsPageModel PageModel => BindingContext as ReviewsPageModel;

        public ReviewsPage ()
		{
			InitializeComponent ();
		}

        private void ReviewItemAppearing(object sender, ItemVisibilityEventArgs e)
        {
            if (PageModel.Reviews.Count == 0)
                return;

            if ((MovieReview)e.Item == PageModel.Reviews[PageModel.Reviews.Count - 1])
                PageModel.LoadNextPageCommand.Execute(null);
        }

        private void ReviewSelected(object sender, EventArgs e)
        {
            ((ListView)sender).SelectedItem = null;
        }
    }
}