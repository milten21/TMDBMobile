using System;
using System.Windows.Input;
using PropertyChanged;
using TMDBMobile.Core.Actions;
using TMDBMobile.Core.Model;
using Xamarin.Forms;

namespace TMDBMobile.Core.PageModels
{
    public enum ProfilePageState
    {
        Loaded,
        Loading,
        Error
    }

    [AddINotifyPropertyChangedInterface]
    public class ProfilePageModel : PageModel
    {
        public ICommand LogoutCommand { get; }
        public ICommand LoadProfileCommand { get; }

        public ProfilePageState State { get; set; }

        public bool IsAuthenticated { get; set; }
        public bool IsLoadingProfile { get; set; }
        public Profile Profile { get; set; }

        public string ErrorMessage { get; set; }

        public ProfilePageModel(IAppStoreContainer storeContainer, DataActionCreator dataActionCreator)
        {
            var store = storeContainer.Store;

            store.Subscribe(s => 
            {
                IsAuthenticated = string.IsNullOrEmpty(s.AuthenticationState.SessionId);
                IsLoadingProfile = s.DataState.IsLoadingProfile;
                Profile = s.DataState.Profile;
                ErrorMessage = s.DataState.ProfileLoadError?.Message;

                if (IsLoadingProfile)
                    State = ProfilePageState.Loading;
                else if (s.DataState.ProfileLoadError != null)
                    State = ProfilePageState.Error;
                else
                    State = ProfilePageState.Loaded;
            });

            LoadProfileCommand = new Command(async () => 
            {
                if (IsLoadingProfile)
                    return;

                await store.Dispatch(dataActionCreator.LoadProileAction);
            });

            LogoutCommand = new Command(() =>
            {
                store.Dispatch(new LogoutAction());
            });
        }

        protected override void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);

            if (Profile == null && !IsLoadingProfile)
                LoadProfileCommand.Execute(null);
        }
    }
}
