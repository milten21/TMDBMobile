using RestSharp;
using TMDBMobile.Core.Model;
using TMDBMobile.Core.Redux;
using TMDBMobile.Core.Services;
using TMDBMobile.Core.States;

namespace TMDBMobile.Core.Actions
{
    public class AuthenticationActionCreator
    {
        private ITMDBService TMDBService { get; }
        private Store<AppState> Store { get; }

        public Store<AppState>.AsyncActionNeedsParam<Credentials> LoginAction { get; private set; }

        public AuthenticationActionCreator(ITMDBService tmdbService, IAppStoreContainer storeContainer)
        {
            TMDBService = tmdbService;
            Store = storeContainer.Store;

            LoginAction = Store.CreateAsyncActionVoid<Credentials>(async(dispatcher, getState, credentials) =>
            {
                var state = getState().DiscoverState;

                dispatcher(new LoginStarted());

                var tokenRequestResponse = await TMDBService.CreateTokenRequest();

                if (!tokenRequestResponse.IsSuccessful)
                {
                    dispatcher(new LoginFailedAction
                    {
                        StatusMessage = tokenRequestResponse.Data.StatusMessage 
                    });

                    return;
                }

                var token = tokenRequestResponse.Data.RequestToken;

                var validationResponse = await TMDBService.ValidateToken(credentials.Username, credentials.Password, token);
                
                if (!validationResponse.IsSuccessful)
                {
                    dispatcher(new LoginFailedAction
                    {
                        StatusMessage = validationResponse.Data.StatusMessage
                    });

                    return;
                }

                token = validationResponse.Data.RequestToken;

                var sessionResponse = await TMDBService.CreateSession(token);
                
                if (!sessionResponse.IsSuccessful)
                {
                    dispatcher(new LoginFailedAction
                    {
                        StatusMessage = validationResponse.Data.StatusMessage
                    });

                    return;
                }

                dispatcher(new LoggedInAction
                {
                    SessionId = sessionResponse.Data.SessionId
                });
            });
        }
    }
}
