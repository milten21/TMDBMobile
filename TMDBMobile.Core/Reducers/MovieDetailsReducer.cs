using System;
using System.Collections.Generic;
using TMDBMobile.Core.Actions;
using TMDBMobile.Core.Redux;
using TMDBMobile.Core.States;

namespace TMDBMobile.Core.Reducers
{
    public class Asss
    {
        
    }

    public static class MovieDetailsReducer 
    {
        public static SimpleReducer<MovieDetailsState> GetReducer()
        {
            
            return new SimpleReducer<MovieDetailsState>()
                .When<StartLoadingMovieDetailts>((state, action) =>
                {
                    state.CancellationToken?.Cancel();
                    state.CancellationToken = action.CancellationToken;
                    state.MovieId = action.MovieId;
                    state.MovieAccountState = null;
                    state.IsLoading = true;

                    return state;
                })
                .When<MovieAccountStateLoaded>((state, action) =>
                {
                    state.MovieAccountState = action.AccountState;
                    state.IsLoading = false;
                    
                    return state;
            })
                .When<UpdateAccountState>((state, action) => 
            {
                if (state.MovieAccountState == null)
                    return state;

                state.MovieAccountState = new Model.MovieAccountState
                {
                    Id = state.MovieAccountState.Id,
                    Favorite = action.IsFavorite ?? state.MovieAccountState.Favorite,
                    Watchlist = state.MovieAccountState.Watchlist
                };

                if (!action.Rate.HasValue)
                    return state;

                state.MovieAccountState.Rated = new Dictionary<string, object> { { "value", action.Rate} };

                return state;
            });
        }
    }

}
