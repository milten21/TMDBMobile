using System.Linq;
using FreshMvvm;
using TMDBMobile.Core.Actions;
using TMDBMobile.Core.PageModels;
using TMDBMobile.Core.Pages;
using TMDBMobile.Core.Reducers;
using TMDBMobile.Core.Redux;
using TMDBMobile.Core.Services;
using TMDBMobile.Core.States;
using TMDBMobile.Core.Utils;
using Xamarin.Forms;

namespace TMDBMobile
{
    // Workaround to avoid using static instance and have possibility
    //  to use Store inside IoC container
    public interface IAppStoreContainer
    {
        Store<AppState> Store { get; }
    }

    public class AppStoreContainer : IAppStoreContainer
    {
        public Store<AppState> Store { get; private set; }

        public AppStoreContainer()
        {
            var reducer = new CompositeReducer<AppState>()
                .Part(s => s.SearchState, SearchReducer.GetReducer())
                .Part(s => s.DiscoverState, DiscoverReducer.GetReducer())
                .Part(s => s.AuthenticationState, AuthenticationReducer.GetReducer())
                .Part(s => s.FavoriteState, FavouriteReducer.GetReducer())
                .Part(s => s.DataState, DataReducer.GetReducer())
                .Part(s => s.MovieDetailsState, MovieDetailsReducer.GetReducer())
                .Part(s => s.ReviewsState, ReviewsReducer.GetReducer());

            Store = new Store<AppState>(reducer);
        }
    }

    public partial class App : Application
    {
        private FreshTabbedNavigationContainer _tabbedPage;

        public App()
        {
            InitializeComponent();

            RegisterServices();

            _tabbedPage = new FreshTabbedNavigationContainer();

            if (Device.OS == TargetPlatform.iOS)
            {
                _tabbedPage.AddTab<FavoriteMoviesPageModel>("Favorite", "starFilled");
                _tabbedPage.AddTab<SearchPageModel>("Search", "searchIcon");
            }
            else
            {
                _tabbedPage.AddTab<FavoriteMoviesPageModel>("Favorite", null);
                _tabbedPage.AddTab<SearchPageModel>("Search", null);
            }

            var store = FreshIOC.Container.Resolve<IAppStoreContainer>().Store;

            store.Dispatch(FreshIOC.Container.Resolve<DataActionCreator>().LoadGenresAction);

            TryAddProfilePage(store);

            MainPage = _tabbedPage;
        }

        private void TryAddProfilePage(Store<AppState> store)
        {
            var state = store.GetState();

            if (string.IsNullOrEmpty(store.GetState().AuthenticationState.SessionId))
                return;

            AddProfilePage();
        }

        internal void AddProfilePage()
        {
            if (_tabbedPage.TabbedPages.FirstOrDefault(p => p is ProfilePage) != null)
                return;

            if (Device.OS == TargetPlatform.iOS)
                _tabbedPage.AddTab<ProfilePageModel>("Profile", "profile");
            else
                _tabbedPage.AddTab<ProfilePageModel>("Profile", null);
        }

        internal void RemoveProfilePage()
        {
            if (_tabbedPage.TabbedPages.FirstOrDefault(p => p is ProfilePage) != null)
                _tabbedPage.RemoveTab<ProfilePage>();
        }

        private void RegisterServices()
        {

#if DEBUG
            FreshIOC.Container.Register<IConfigurationService, DebugConfigurationService>();
#else
            // Formality, there won't be any release version
            FreshIOC.Container.Register<IConfigurationService, ReleaseConfigurationService>();
#endif

            FreshIOC.Container.Register<ITMDBService, TMDBService>();

            FreshIOC.Container.Register<IAppStoreContainer, AppStoreContainer>();
        }
    }
}
