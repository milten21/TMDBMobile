using System;
using System.Collections.Generic;
using TMDBMobile.Core.Model;

namespace TMDBMobile.Core.Actions
{
    public struct StartLoadingDiscoverPage { }

    public struct FailedToLoadDiscoverPage
    {
        public Exception Exception { get; set; }
    }

    public struct DiscoverPageLoaded
    {
        public int Page { get; set; }
        public int TotalPages { get; set; }
        public List<Movie> NewPage { get; set; }
    }
}
