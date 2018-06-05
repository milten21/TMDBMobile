using TMDBMobile.Core.Model;
using TMDBMobile.Core.Redux;
using TMDBMobile.Core.States;
using Xamarin.Forms;

namespace TMDBMobile.Core.PageModels
{
    public class MovieDetailsPageModel : PageModel
    {
        public string Title { get; set; }

        public Movie Movie { get; set; }

        private Store<AppState> Store { get; }

        public MovieDetailsPageModel(IAppStoreContainer storeContainer)
        {
            Store = storeContainer.Store;
        }

        public override void Init(object initData)
        {
            if (!(initData is Movie movie))
                return;
            
            Movie = movie;
            
            Title = Movie.Title;

        }
    }
}
