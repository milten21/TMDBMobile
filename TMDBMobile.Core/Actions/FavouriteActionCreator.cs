using TMDBMobile.Core.Redux;
using TMDBMobile.Core.Services;
using TMDBMobile.Core.States;

namespace TMDBMobile.Core.Actions
{
    public class FavouriteActionCreator
    {
        private ITMDBService TMDBService { get; }
        private Store<AppState> Store { get; }

        public Store<AppState>.AsyncAction LoadNextPageAction { get; private set; }

        public FavouriteActionCreator(ITMDBService tmdbService, IAppStoreContainer storeContainer)
        {
            TMDBService = tmdbService;
            Store = storeContainer.Store;

            LoadNextPageAction = async (dispatcher, getState) =>
            {
                var state = getState().FavoriteState;

                if (state.TotalPages != 0 && state.LastLoadedPage == state.TotalPages)
                    return;

                dispatcher(new StartLoadingFavouritePage());

                var response = await TMDBService.GetFavoriteMovies(++state.LastLoadedPage);

                if (response.IsSuccessful)
                {
                    dispatcher(new FavouritePageLoaded
                    {
                        Page = response.Data.Page,
                        TotalPages = response.Data.TotalPages,
                        NewPage = response.Data.Results
                    });

                    return;
                }

                dispatcher(new FailedToLoadFavouritePage
                {
                    Exception = new System.Exception(response.Data.StatusMessage)
                });
            };
        }
    }
}
