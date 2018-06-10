using System;
using System.Collections.Generic;
using System.Text;
using TMDBMobile.Core.Redux;
using TMDBMobile.Core.Services;
using TMDBMobile.Core.States;

namespace TMDBMobile.Core.Actions
{
    public class ReviewActionCreator
    {
        private ITMDBService TMDBService { get; }
        private Store<AppState> Store { get; }

        public Store<AppState>.AsyncActionNeedsParam<int> LoadReviewsAction { get; private set; }

        public ReviewActionCreator(ITMDBService tmdbService, IAppStoreContainer storeContainer)
        {
            TMDBService = tmdbService;
            Store = storeContainer.Store;

            LoadReviewsAction = Store.CreateAsyncActionVoid<int>(async (dispatcher, getState, movieId) =>
            {
                var state = getState().ReviewsState;

                if (state.TotalPages != 0 && state.LastLoadedPage == state.TotalPages && state.MovieId == movieId)
                    return;

                dispatcher(new StartLoadingReviews
                {
                    MovieId = movieId
                });

                var response = await TMDBService.GetReviews(movieId, ++state.LastLoadedPage);

                if (response.IsSuccessful)
                {
                    dispatcher(new ReviewsLoaded
                    {
                        Page = response.Data.Page,
                        TotalPages = response.Data.TotalPages,
                        Result = response.Data.Results
                    });

                    return;
                }

                dispatcher(new ReviewsLoadError
                {
                    Exception = new Exception(response.Data.StatusMessage)
                });
            });
        }
    }
}
