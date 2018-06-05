using System;
using System.Windows.Input;
using TMDBMobile.Core.Actions;
using Xamarin.Forms;

namespace TMDBMobile.Core.PageModels
{
    public class ProfilePageModel : PageModel
    {
        public ICommand LogoutCommand { get; }
        
        public ProfilePageModel(IAppStoreContainer storeContainer)
        {
            var store = storeContainer.Store;

            LogoutCommand = new Command(() =>
            {
                store.Dispatch(new LogoutAction());
            });
        }
    }
}
