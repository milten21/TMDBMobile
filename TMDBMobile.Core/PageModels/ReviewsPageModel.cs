using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TMDBMobile.Core.Actions;
using TMDBMobile.Core.Model;
using TMDBMobile.Core.Redux;
using TMDBMobile.Core.States;
using Xamarin.Forms;

namespace TMDBMobile.Core.PageModels
{
    public enum ReviewsPageState
    {
        LoadingOrLoaded,
        Error,
        Empty
    }

    [AddINotifyPropertyChangedInterface]
    public class ReviewsPageModel : PageModel
    {
        public bool IsLoading { get; set; }

        public List<MovieReview> Reviews { get; set; }

        public Movie Movie { get; set; }
        public Store<AppState> Store { get; set; }
        public ReviewActionCreator ReviewActionCreator { get; set; }

        public ReviewsPageState State { get; set; }
        public string Error { get; set; }

        public ICommand DismissCommand { get; set; }
        public ICommand LoadNextPageCommand { get; set; }

        public ReviewsPageModel(ReviewActionCreator reviewActionCreator, IAppStoreContainer storeContainer)
        {
            ReviewActionCreator = reviewActionCreator;
            Store = storeContainer.Store;

            Store.Subscribe(Reduce);

            Store.Dispatch(new StartLoadingReviews());

            DismissCommand = new Command(() =>
            {
                CoreMethods.PopPageModel(true);
            });

            LoadNextPageCommand = new Command(() =>
            {
                Store.Dispatch(ReviewActionCreator.LoadReviewsAction);
            });
        }

        private void Reduce(AppState s)
        {
            var state = s.ReviewsState;

            IsLoading = state.IsLoadingReviews;
            Error = null;

            if (IsLoading)
            {
                Reviews = null;
                State = ReviewsPageState.LoadingOrLoaded;
                return;
            }

            if (state.Error != null)
            {
                Error = state.Error.Message;
                State = ReviewsPageState.Error;
                return;
            }

            if (state.Reviews?.Count == 0)
            {
                State = ReviewsPageState.Empty;
                return;
            }

            Reviews = state.Reviews;
            State = ReviewsPageState.LoadingOrLoaded;
        }

        public override void Init(object initData)
        {
            if (!(initData is Movie movie))
                return;

            Movie = movie;   
        }
    }
}
