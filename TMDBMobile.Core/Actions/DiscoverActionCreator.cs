using TMDBMobile.Core.Redux;
using TMDBMobile.Core.Services;
using TMDBMobile.Core.States;

namespace TMDBMobile.Core.Actions
{
    public class DiscoverActionCreator
    {
        private ITMDBService TMDBService { get; }
        private Store<AppState> Store { get; }

        public Store<AppState>.AsyncAction LoadNextPageAction { get; private set; }

        public DiscoverActionCreator(ITMDBService tmdbService, IAppStoreContainer storeContainer)
        {
            TMDBService = tmdbService;
            Store = storeContainer.Store;

            LoadNextPageAction = async (dispatcher, getState) =>
            {
                var state = getState().DiscoverState;

                if (state.TotalPages != 0 && state.LastLoadedPage == state.TotalPages)
                    return;

                dispatcher(new StartLoadingDiscoverPage());

                var response = await TMDBService.Discover(++state.LastLoadedPage);

                if (response.IsSuccessful)
                {
                    dispatcher(new DiscoverPageLoaded
                    {
                        Page = response.Data.Page,
                        TotalPages = response.Data.TotalPages,
                        NewPage = response.Data.Results
                    });

                    return;
                }

                dispatcher(new FailedToLoadDiscoverPage
                {
                    Exception = response.ErrorException
                });
            };
        }
    }
}
