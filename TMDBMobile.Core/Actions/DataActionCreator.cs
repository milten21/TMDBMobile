using System;
using TMDBMobile.Core.Redux;
using TMDBMobile.Core.Services;
using TMDBMobile.Core.States;

namespace TMDBMobile.Core.Actions
{
    public class DataActionCreator
    {
        private ITMDBService TMDBService { get; }
        private Store<AppState> Store { get; }

        public Store<AppState>.AsyncAction LoadGenresAction { get; private set; }

        public DataActionCreator(ITMDBService tmdbService, IAppStoreContainer storeContainer)
        {
            TMDBService = tmdbService;
            Store = storeContainer.Store;

            LoadGenresAction = async (dispatcher, getState) =>
            {
                var response = await TMDBService.GetGenres();

                if (!response.IsSuccessful)
                {
                    dispatcher(new GenresLoaded());
                }
                else
                {
                    dispatcher(new GenresLoaded
                    {
                        Genres = response.Data.Genres
                    });
                }
            };
        }
    }
}
