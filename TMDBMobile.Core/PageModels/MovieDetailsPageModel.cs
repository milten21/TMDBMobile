using System.Collections.Generic;
using System.Windows.Input;
using PropertyChanged;
using TMDBMobile.Core.Actions;
using TMDBMobile.Core.Model;
using TMDBMobile.Core.Redux;
using TMDBMobile.Core.States;
using Xamarin.Forms;
using FreshMvvm;
using System;

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
        public ICommand ReadReviewsCommand { get; set; }

        private MovieDetailActionCreator MovieDetailActionCreator { get; }
        private ReviewActionCreator ReviewActionCreator { get; }
        private Store<AppState> Store { get; }

        public MovieDetailsPageModel(IAppStoreContainer storeContainer, 
            MovieDetailActionCreator movieDetailActionCreator,
            ReviewActionCreator reviewActionCreator)
        {
            MovieDetailActionCreator = movieDetailActionCreator;
            ReviewActionCreator = reviewActionCreator;
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

            ReadReviewsCommand = new FreshAwaitCommand(async (parameter, tcs) =>
            {
                await CoreMethods.PushPageModel<ReviewsPageModel>(Movie, true, true);
                tcs.SetResult(true);

                await Store.Dispatch(ReviewActionCreator.LoadReviewsAction(Movie.Id));
            });
        }

        public override void Init(object initData)
        {
            if (!(initData is Movie movie))
                return;

            Movie = movie;

            Title = Movie.Title;

        }

        protected override async void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);

            await Store.Dispatch(MovieDetailActionCreator.LoadMovieDetailsAction(Movie.Id));
        }
    }
}
