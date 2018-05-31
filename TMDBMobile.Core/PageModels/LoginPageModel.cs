using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TMDBMobile.Core.Actions;
using Xamarin.Forms;

namespace TMDBMobile.Core.PageModels
{
    [AddINotifyPropertyChangedInterface]
    public class LoginPageModel : PageModel
    {
        public bool IsLoggingIn { get; set; }

        public string Username { get; set; }
        public string Password { get; set; }

        public ICommand PopCommand { get; set; }
        public ICommand LoginCommand { get; set; }

        public LoginPageModel(IAppStoreContainer storeContainer, AuthenticationActionCreator authenticationActionCreator)
        {
            var store = storeContainer.Store;

            store.Subscribe(s =>
            {
                IsLoggingIn = s.AuthenticationState.IsLoggingIn;

                if (s.AuthenticationState.Exception != null)
                {
                    CoreMethods.DisplayAlert("An error occured",
                        s.AuthenticationState.Exception.Message, "OK");

                    return;
                }

                if (!string.IsNullOrEmpty(s.AuthenticationState.SessionId))
                    PopCommand.Execute(null);

            });

            PopCommand = new Command(() =>
            {
                CoreMethods.PopPageModel(true, true);
            });

            LoginCommand = new Command(() =>
            {
                if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
                {
                    CoreMethods.DisplayAlert("An error occured", 
                        "All fields are required", "OK");

                    return;
                }

                store.Dispatch(authenticationActionCreator.LoginAction(new Model.Credentials
                {
                    Password = Password,
                    Username = Username
                }));
            });
        }
    }
}
