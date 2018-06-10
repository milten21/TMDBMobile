using System;
using FreshMvvm;
using TMDBMobile.Core.Actions;
using TMDBMobile.Core.Redux;
using TMDBMobile.Core.Services;
using TMDBMobile.Core.States;
using Xamarin.Forms;

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


                    CredentialsManager.Instance.SaveAccount(state.SessionId);
                    FreshIOC.Container.Resolve<ITMDBService>().SetSessionId(state.SessionId);

                    (Application.Current as App)?.AddProfilePage();

                    return state;
                })
                .When<LoginFailedAction>((state, action) =>
                {
                    state.Exception = new Exception(action.StatusMessage);
                    state.IsLoggingIn = false;


                    return state;
                })
                .When<LogoutAction>((state, action) =>
                {
                    state.RequestToken = null;
                    state.SessionId = null;

                    CredentialsManager.Instance.DeleteAccount();
                    (Application.Current as App)?.RemoveProfilePage();

                    return state;
                })
                .When<InitStoreAction>((state, action) =>
                {
                    state.SessionId = CredentialsManager.Instance.SessionId;
                    FreshIOC.Container.Resolve<ITMDBService>().SetSessionId(state.SessionId);

                    return state;
                });
        }
    }
}
