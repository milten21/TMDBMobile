using System.Collections.Generic;
using TMDBMobile.Core.Actions;
using TMDBMobile.Core.Model;
using TMDBMobile.Core.Redux;
using TMDBMobile.Core.States;

namespace TMDBMobile.Core.Reducers
{
    public static class FavouriteReducer
    {
        public static SimpleReducer<FavoriteState> GetReducer()
        {
            return new SimpleReducer<FavoriteState>()
                .When<StartLoadingFavouritePage>((state, action) =>
                {
                    state.IsLoadingPage = true;
                    state.Exception = null;

                    return state;
                })
                .When<FavouritePageLoaded>((state, action) =>
                {
                    if (state.LastLoadedPage >= action.Page)
                        return state;

                    state.LastLoadedPage = action.Page;
                    state.TotalPages = action.TotalPages;
                    state.IsLoadingPage = false;

                    if (state.Movies == null)
                        state.Movies = new List<Movie>();

                    state.Movies.AddRange(action.NewPage);

                    return state;
                })
                .When<FailedToLoadFavouritePage>((state, action) =>
                {
                    state.IsLoadingPage = false;
                    state.Exception = action.Exception;

                    return state;
                })
                .When<ReloadFavoritesAction>((state, action) =>
                {
                    state.Movies = null;
                    state.Exception = null;
                    state.LastLoadedPage = 0;
                    state.TotalPages = 0;

                    return state;
                });
        }
    }
}
