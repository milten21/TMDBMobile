using FreshMvvm;
using TMDBMobile.Core.Actions;
using TMDBMobile.Core.PageModels;
using TMDBMobile.Core.Reducers;
using TMDBMobile.Core.Redux;
using TMDBMobile.Core.Services;
using TMDBMobile.Core.States;
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
                .Part(s => s.AuthenticationState, AuthenticationReducer.GetReducer());

            Store = new Store<AppState>(reducer);
        }
    }

    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            RegisterServices();
            RegisterActionCreators();

            var tabbedPage = new FreshTabbedNavigationContainer();
            tabbedPage.AddTab<FavoriteMoviesPageModel>("Favorites", null);
            tabbedPage.AddTab<SearchPageModel>("Search", null);
            MainPage = tabbedPage;
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

        private void RegisterActionCreators()
        {
            var tmdbService = FreshIOC.Container.Resolve<ITMDBService>();

            // TODO: Find out a solution
            // Registred as multiinstance, gets crash when trying to register as singleton
            FreshIOC.Container.Register(new SearchActionCreator(tmdbService,
                FreshIOC.Container.Resolve<IAppStoreContainer>()));
            FreshIOC.Container.Register(new DiscoverActionCreator(tmdbService,
                FreshIOC.Container.Resolve<IAppStoreContainer>()));
        }
    }
}
