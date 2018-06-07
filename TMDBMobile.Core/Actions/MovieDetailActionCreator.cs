using System.Threading;
using System.Threading.Tasks;
using TMDBMobile.Core.Model;
using TMDBMobile.Core.Redux;
using TMDBMobile.Core.Services;
using TMDBMobile.Core.States;

namespace TMDBMobile.Core.Actions
{
    public class MovieDetailActionCreator
    {
        private ITMDBService TMDBService { get; }
        private Store<AppState> Store { get; }

        public Store<AppState>.AsyncActionNeedsParam<int> LoadMovieDetailsAction { get; private set; }
        public Store<AppState>.AsyncActionNeedsParam<FavoriteMovieArgs> FavoriteMovieAction { get; set; }
        public Store<AppState>.AsyncActionNeedsParam<RateMovieArgs> RateMovieAction { get; set; }
                               
        public MovieDetailActionCreator(ITMDBService tmdbService, IAppStoreContainer storeContainer)
        {
            TMDBService = tmdbService;
            Store = storeContainer.Store;

            FavoriteMovieAction = Store.CreateAsyncActionVoid<FavoriteMovieArgs>(async (dispatcher, getState, args) =>
            {
                dispatcher(new UpdateAccountState
                {
                    IsFavorite = args.IsFavorite
                });

                var response = await TMDBService.FavoriteMovie(args.IsFavorite, args.MovieId);

                if (!response.IsSuccessful)
                    return;

                dispatcher(new ReloadFavoritesAction());
            });

            RateMovieAction = Store.CreateAsyncActionVoid<RateMovieArgs>(async (dispatcher, getState, args) =>
            {
                dispatcher(new UpdateAccountState
                {
                    Rate = args.Rate
                });

                var response = await TMDBService.RateMovie(args.Rate, args.MovieId);
            });

            LoadMovieDetailsAction = Store.CreateAsyncActionVoid<int>(async (dispatcher, getState, movieId) =>
            {
                if (string.IsNullOrEmpty(getState().AuthenticationState.SessionId))
                    return;

                var cancellationToken = new CancellationTokenSource();

                dispatcher(new StartLoadingMovieDetailts
                {
                    MovieId = movieId,
                    CancellationToken = cancellationToken
                });

                
                // null in case of failure, don't really care about excec errors handling here

                var loadTask = Task.Factory.StartNew(async () =>
                {
                    var response = await TMDBService.AccountStateForMovie(movieId);

                    dispatcher(new MovieAccountStateLoaded
                    {
                        AccountState = response.Data 
                    });
                }, cancellationToken.Token);

                try
                {
                    await loadTask;
                }
                catch (TaskCanceledException)
                {
                    // nothing to do here, user has chonsen another movie
                }

            });
        }
    }
}
