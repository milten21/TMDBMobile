using FreshMvvm;
using PropertyChanged;
using System.Collections.Generic;
using System.Windows.Input;
using TMDBMobile.Core.Actions;
using TMDBMobile.Core.Model;
using TMDBMobile.Core.States;
using Xamarin.Forms;

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
        public ICommand LoadFavoritePageCommand { get; }

        private Movie _selectedMovie;
        public Movie SelectedMovie
        {
            get => _selectedMovie;
            set
            {
                _selectedMovie = value;
                RaisePropertyChanged();

                if (_selectedMovie == null)
                    return;

                CoreMethods.PushPageModel<MovieDetailsPageModel>(SelectedMovie);

                SelectedMovie = null;
            }
        }

        public List<Movie> Movies { get; set; }

        public bool IsSearching { get; set; }

        public FavoriteMoviesPageModel(IAppStoreContainer storeContainer, FavouriteActionCreator favouriteActionCreator)
        {
            var store = storeContainer.Store;

            LoadFavoritePageCommand = new Command(async () =>
            {
                if (IsSearching)
                    return;

                await store.Dispatch(favouriteActionCreator.LoadNextPageAction);
            });
            
            store.Subscribe(s =>
            {
                IsSearching = s.FavoriteState.IsLoadingPage;
                ReduceFavouriteState(s.FavoriteState);

                if (!string.IsNullOrEmpty(s.AuthenticationState.SessionId) && s.AuthenticationState.Exception == null)
                {
                    if (s.FavoriteState.LastLoadedPage == 0 && !IsSearching && s.FavoriteState.Exception == null)
                        LoadFavoritePageCommand.Execute(null);

                    if (Movies?.Count != 0 || IsSearching)
                        State = FavoritePageStates.Authenticated;
                    else
                        State = FavoritePageStates.Empty;
                }
                else
                    State = FavoritePageStates.NotAuthenticated;
            });

            PresentLoginPage = new FreshAwaitCommand(async (parameter, tcs) =>
            {
                await CoreMethods.PushPageModel<LoginPageModel>(null, true, true);
                tcs.SetResult(true);
            });
        }

        private void ReduceFavouriteState(FavoriteState state)
        {
            if (state.Movies != null)
                Movies = new List<Movie>(state.Movies);
        }
    }
}
