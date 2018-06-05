using System;
using System.Linq;
using Xamarin.Auth;

namespace TMDBMobile.Core.Services
{
    public class CredentialsManager
    {
        private static readonly Lazy<CredentialsManager> Lazy = new Lazy<CredentialsManager>(() => new CredentialsManager());
        public static CredentialsManager Instance { get { return Lazy.Value; } }

        private string AppName => "TMDBMobile";
        private Account Account => AccountStore.Create().FindAccountsForService(AppName).FirstOrDefault();

        public string SessionId => Account?.Properties["sessionId"];

        public void SaveAccount(string sessionId)
        {
            if (string.IsNullOrWhiteSpace(sessionId))
                return;

            var account = new Account("user");
            account.Properties.Add("sessionId", sessionId);

            AccountStore.Create().Save(account, AppName);  // If an Account was previously saved, calling the Save method again will overwrite it.
        }
        public void DeleteAccount()
        {
            if (Account != null)
                AccountStore.Create().Delete(Account, AppName);
        }
    }
}
