using TMDBMobile.Core.Actions;
using TMDBMobile.Core.Redux;
using TMDBMobile.Core.States;

namespace TMDBMobile.Core.Reducers
{
    public static class DataReducer
    {
        public static SimpleReducer<DataState> GetReducer()
        {
            return new SimpleReducer<DataState>()
                .When<GenresLoaded>((state, action) =>
                {
                    if (action.Genres != null)
                        state.Genres = action.Genres;
                
                    return state;
                })
                .When<StartLoadingProfile>((state, action) =>
                {
                    state.IsLoadingProfile = true;
                    state.ProfileLoadError = null;

                    return state;
                })
                .When<ProfileLoaded>((state, action) =>
                {
                    state.Profile = action.Profile;
                    state.IsLoadingProfile = false;

                    return state;
                })
                .When<FailedLoadProfile>((state, action) =>
                {
                    state.ProfileLoadError = action.Exception;
                    state.IsLoadingProfile = false;

                    return state;
                });
        }
    }
}
