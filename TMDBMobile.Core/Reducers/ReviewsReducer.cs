using System;
using System.Collections.Generic;
using System.Text;
using TMDBMobile.Core.Actions;
using TMDBMobile.Core.Model;
using TMDBMobile.Core.Redux;
using TMDBMobile.Core.States;

namespace TMDBMobile.Core.Reducers
{
    public static class ReviewsReducer
    {
        public static SimpleReducer<ReviewsState> GetReducer()
        {
            return new SimpleReducer<ReviewsState>()
                 .When<StartLoadingReviews>((state, action) =>
                 {
                     state.IsLoadingReviews = true;
                     state.MovieId = action.MovieId;
                     state.LastLoadedPage = 0;
                     state.Reviews = null;
                     state.TotalPages = 0;

                     return state;
                 })
                 .When<ReviewsLoaded>((state, action) =>
                 {
                     state.IsLoadingReviews = false;
                     state.LastLoadedPage = action.Page;
                     state.TotalPages = action.TotalPages;

                     if (state.Reviews == null)
                         state.Reviews = new List<MovieReview>();

                     state.Reviews.AddRange(action.Result);

                     return state;
                 })
                 .When<ReviewsLoadError>((state, action) =>
                 {
                     state.IsLoadingReviews = false;
                     state.Error = action.Exception;

                     return state;
                 });
        }
    }
}
