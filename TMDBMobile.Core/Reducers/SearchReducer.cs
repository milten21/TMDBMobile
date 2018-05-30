using System.Collections.Generic;
using TMDBMobile.Core.Actions;
using TMDBMobile.Core.Model;
using TMDBMobile.Core.Redux;
using TMDBMobile.Core.States;

namespace TMDBMobile.Core.Reducers
{
    public static class SearchReducer
    {
        public static SimpleReducer<SearchState> GetReducer()
        {
            return new SimpleReducer<SearchState>()
            .When<ResetSearchResults>((state, action) =>
            {
                state.Movies = new List<Movie>();
                state.Query = action.Query;
                state.LastLoadedPage = 0;
                state.TotalPages = 0;
                state.IsSearching = false;

                return state;
            })
            .When<StartLoadingPage>((state, action) =>
            {
                state.IsSearching = true;
                state.Exception = null;

                return state;
            })
            .When<PageLoaded>((state, action) =>
            {
                if (state.LastLoadedPage >= action.Page)
                    return state;

                state.LastLoadedPage = action.Page;
                state.TotalPages = action.TotalPages;
                state.IsSearching = false;
                state.Movies.AddRange(action.NewPage);

                return state;
            })
            .When<FailedToLoadPage>((state, action) =>
            {
                state.IsSearching = false;
                state.Exception = action.Exception;

                return state;
            });
        }
    }
}
