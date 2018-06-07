using System.Collections.Generic;
using System.Windows.Input;
using PropertyChanged;
using TMDBMobile.Core.Actions;
using TMDBMobile.Core.Model;
using TMDBMobile.Core.Redux;
using TMDBMobile.Core.States;
using Xamarin.Forms;

namespace TMDBMobile.Core.PageModels
{
    [AddINotifyPropertyChangedInterface]
    public class MovieDetailsPageModel : PageModel
    {
        public string Title { get; set; }

        public Movie Movie { get; set; }

        public bool IsFavorite { get; set; }
        public bool IsLoadingDetails { get; set; }
        public bool IsAuthenticated { get; set; }

        public int Rate { get; set; }

        public ICommand FavoriteCommand { get; set; }
        public ICommand PositiveRateCommand { get; set; }
        public ICommand NegativeRateCommand { get; set; }

        private MovieDetailActionCreator MovieDetailActionCreator { get; }
        private Store<AppState> Store { get; }

        public MovieDetailsPageModel(IAppStoreContainer storeContainer, MovieDetailActionCreator movieDetailActionCreator)
        {
            MovieDetailActionCreator = movieDetailActionCreator;
            Store = storeContainer.Store;

            Store.Subscribe(s => 
            {
                IsAuthenticated = !string.IsNullOrEmpty(s.AuthenticationState.SessionId);
                IsLoadingDetails = s.MovieDetailsState.IsLoading;

                if (s.MovieDetailsState.MovieId != Movie.Id || IsLoadingDetails)
                    return;

                if (s.MovieDetailsState.MovieAccountState == null)
                {
                    // an error occured, can be handled here
                    return;
                }
                var accountState = s.MovieDetailsState.MovieAccountState;

                IsFavorite = accountState.Favorite;

                if (!(accountState.Rated is Dictionary<string, object> rated))
                    return;

                // workaround, unboxing doesn't work
                Rate = int.Parse(rated["value"].ToString());
            });

            PositiveRateCommand = new Command(async () =>
            {
                await Store.Dispatch(MovieDetailActionCreator.RateMovieAction(new RateMovieArgs
                {
                    MovieId = Movie.Id,
                    Rate = 10
                }));
            });

            NegativeRateCommand = new Command(async () =>
            {
                await Store.Dispatch(MovieDetailActionCreator.RateMovieAction(new RateMovieArgs
                {
                    MovieId = Movie.Id,
                    Rate = 1
                }));
            });

            FavoriteCommand = new Command(async () =>
            {
                await Store.Dispatch(MovieDetailActionCreator.FavoriteMovieAction(new FavoriteMovieArgs
                {
                    IsFavorite = !IsFavorite,
                    MovieId = Movie.Id
                }));
            });
        }

        public override void Init(object initData)
        {
            if (!(initData is Movie movie))
                return;
            
            Movie = movie;
            
            Title = Movie.Title;

            Store.Dispatch(MovieDetailActionCreator.LoadMovieDetailsAction(Movie.Id));
        }
    }
}
