using PropertyChanged;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

using TMDBMobile.Core.Actions;
using TMDBMobile.Core.Model;
using TMDBMobile.Core.Redux;
using TMDBMobile.Core.Services;
using TMDBMobile.Core.States;
using Xamarin.Forms;

namespace TMDBMobile.Core.PageModels
{
    public enum SearchPageState
    {
        Searching,
        Discover,
        Empty,
    }

    [AddINotifyPropertyChangedInterface]
    public class SearchPageModel : PageModel
    {
        public SearchPageState State { get; private set; } = SearchPageState.Discover;
        
        public ICommand LoadDiscoverPageCommand { get; }
        public ICommand LoadNextPageCommand { get; }

        private string _query;
        public string Query
        {
            get => _query;
            set
            {
                _query = value;
                RaisePropertyChanged();

                LoadNextPageCommand.Execute(null);
            }
        }

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


        public bool IsSearching { get; set; }

        public List<Movie> Movies { get; set; }
        public List<Movie> DiscoverMovies { get; set; }

        private CancellationTokenSource _cancelSearch = new CancellationTokenSource();
        private Store<AppState> _store;

        public SearchPageModel(SearchActionCreator searchActionCreator,
            DiscoverActionCreator discoverActionCreator, IAppStoreContainer storeContainer)
        {
            _store = storeContainer.Store;

            LoadDiscoverPageCommand = new Command(async () =>
            {
                if (IsSearching)
                    return;
                
                await _store.Dispatch(discoverActionCreator.LoadNextPageAction);
            });

            LoadNextPageCommand = new Command(async () =>
            {
                if (IsSearching)
                    return;
                
                try
                {
                    Interlocked.Exchange(ref _cancelSearch, new CancellationTokenSource()).Cancel();
                    await Task.Delay(500, _cancelSearch.Token)
                    .ContinueWith(async delegate
                    {
                            await _store.Dispatch(searchActionCreator.LoadNextPageAction(Query));
                    },
                    CancellationToken.None,
                    TaskContinuationOptions.OnlyOnRanToCompletion,
                    TaskScheduler.FromCurrentSynchronizationContext());
                }
                catch (TaskCanceledException)
                {
                    // Nothing to do here, user just typed one more character during search
                }
            });

            _store.Subscribe(s =>
            {
                IsSearching = s.SearchState.IsSearching || s.DiscoverState.IsLoadingPage;

                ReduceDiscoverState(s.DiscoverState);
                ReduceSearchState(s.SearchState);
            });

            LoadDiscoverPageCommand.Execute(null);
        }

        private void ReduceDiscoverState(DiscoverState state)
        {
            if (!state.IsLoadingPage && state.Movies != null)
                DiscoverMovies = new List<Movie>(state.Movies);
        }

        private void ReduceSearchState(SearchState state)
        {
            if (!state.IsSearching && state.Movies != null)
                Movies = new List<Movie>(state.Movies);

            if (string.IsNullOrEmpty(Query))
                State = SearchPageState.Discover;
            else if (Movies.Count == 0 && !IsSearching)
                State = SearchPageState.Empty;
            else
                State = SearchPageState.Searching;
        }
    }
}
