using System;
using System.Collections.Generic;
using System.Text;

namespace TMDBMobile.Core.States
{
    public struct AppState
    {
        public AuthenticationState AuthenticationState { get; set; }
        public SearchState SearchState { get; set; }
        public DiscoverState DiscoverState { get; set; }
    }
}
