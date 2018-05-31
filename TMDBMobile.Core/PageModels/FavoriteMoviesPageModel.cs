using FreshMvvm;
using PropertyChanged;
using System.Windows.Input;
using TMDBMobile.Core.States;

namespace TMDBMobile.Core.PageModels
{
    public enum FavoritePageStates
    {
        NotAuthenticated,
        Authenticated,
        Empty
    }

    [AddINotifyPropertyChangedInterface]
    public class FavoriteMoviesPageModel : PageModel
    {
        public FavoritePageStates State { get; set; }

        public ICommand PresentLoginPage { get; set; }

        public FavoriteMoviesPageModel(IAppStoreContainer storeContainer)
        {
            var store = storeContainer.Store;

            store.Subscribe(s =>
            {
                ReduceAuthenticationState(s.AuthenticationState);
            });

            PresentLoginPage = new FreshAwaitCommand(async (parameter, tcs) =>
            {
                await CoreMethods.PushPageModel<LoginPageModel>(null, true, true);
                tcs.SetResult(true);
            });
        }

        private void ReduceAuthenticationState(AuthenticationState state)
        {
            if (!string.IsNullOrEmpty(state.SessionId) && state.Exception == null)
                State = FavoritePageStates.Authenticated;
            else
                State = FavoritePageStates.NotAuthenticated;
        }
    }
}
