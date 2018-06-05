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
        public Store<AppState>.AsyncAction LoadProileAction { get; private set; }

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
                    return;
                }

                dispatcher(new GenresLoaded
                {
                    Genres = response.Data.Genres
                });
            };

            LoadProileAction = async (dispatcher, getState) =>
            {
                var response = await TMDBService.GetProfile();

                dispatcher(new StartLoadingProfile());

                if (!response.IsSuccessful)
                {
                    dispatcher(new FailedLoadProfile
                    {
                        Exception = new Exception("Failed to load profile")
                    });

                    return;
                }

                dispatcher(new ProfileLoaded
                {
                    Profile = response.Data
                });
            };

        }
    }
}
