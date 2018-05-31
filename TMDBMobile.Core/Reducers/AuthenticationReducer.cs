using System;
using TMDBMobile.Core.Actions;
using TMDBMobile.Core.Redux;
using TMDBMobile.Core.States;

namespace TMDBMobile.Core.Reducers
{
    public static class AuthenticationReducer
    {
        public static SimpleReducer<AuthenticationState> GetReducer()
        {
            return new SimpleReducer<AuthenticationState>()
                .When<LoginStarted>((state, action) =>
                {
                    state.IsLoggingIn = true;
                    state.Exception = null;

                    return state;
                })
                .When<LoggedInAction>((state, action) =>
                {
                    // Can store SessionId somewhere on users device locally,
                    // so it could be restored next time the application launched

                    state.Exception = null;
                    state.SessionId = action.SessionId;
                    state.IsLoggingIn = false;

                    return state;
                })
                .When<LoginFailedAction>((state, action) =>
                {
                    state.Exception = new Exception(action.StatusMessage);
                    state.IsLoggingIn = false;

                    return state;
                });
        }
    }
}
