using System;

namespace TMDBMobile.Core.States
{
    public struct AuthenticationState
    {
        public bool IsLoggingIn { get; set; }

        public string RequestToken { get; set; }
        public string SessionId { get; set; }

        public Exception Exception { get; set; }
    }
}
