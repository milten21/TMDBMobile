namespace TMDBMobile.Core.Actions
{
    public struct LoginStarted { }

    public struct LoggedInAction
    {
        public string SessionId { get; set; }
    }

    public struct LoginFailedAction
    {
        public int StatusCode { get; set; }
        public string StatusMessage { get; set; }
    }
}
