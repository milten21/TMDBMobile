namespace TMDBMobile.Core.States
{
    public struct AppState
    {
        public AuthenticationState AuthenticationState { get; set; }
        public SearchState SearchState { get; set; }
        public DiscoverState DiscoverState { get; set; }
        public FavouriteState FavouriteState { get; set; }
    }
}
