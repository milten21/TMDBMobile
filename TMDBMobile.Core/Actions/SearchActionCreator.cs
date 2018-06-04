using TMDBMobile.Core.Redux;
using TMDBMobile.Core.Services;
using TMDBMobile.Core.States;

namespace TMDBMobile.Core.Actions
{
    public class SearchActionCreator
    {
        private ITMDBService TMDBService { get; }
        private Store<AppState> Store { get; }

        public Store<AppState>.AsyncActionNeedsParam<string> LoadNextPageAction { get; private set; }

        public SearchActionCreator(ITMDBService tmdbService, IAppStoreContainer storeContainer)
        {
            TMDBService = tmdbService;
            Store = storeContainer.Store;

            LoadNextPageAction = Store.CreateAsyncActionVoid<string>(async (dispatcher, getState, query) =>
            {
                var state = getState().SearchState;

                if (state.TotalPages != 0 && state.LastLoadedPage == state.TotalPages && query == state.Query)
                    return;

                if (state.Query != query || string.IsNullOrEmpty(query))
                {
                    dispatcher(new ResetSearchResults
                    {
                        Query = query
                    });
                }

                dispatcher(new StartLoadingPage());

                state = getState().SearchState;

                var response = await TMDBService.Search(query, ++state.LastLoadedPage);

                if (response.IsSuccessful)
                {
                    dispatcher(new PageLoaded
                    {
                        Page = response.Data.Page,
                        TotalPages = response.Data.TotalPages,
                        NewPage = response.Data.Results
                    });

                    return;
                }

                dispatcher(new FailedToLoadPage
                {
                    Exception = response.ErrorException
                });
            });
        }
    }
}
