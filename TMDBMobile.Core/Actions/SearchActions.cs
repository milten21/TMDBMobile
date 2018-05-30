using System;
using System.Collections.Generic;
using TMDBMobile.Core.Model;

namespace TMDBMobile.Core.Actions
{
    public struct StartLoadingPage { }

    public struct ResetSearchResults
    {
        public string Query { get; set; }
    }

    public struct FailedToLoadPage
    {
        public Exception Exception { get; set; }
    }

    public struct PageLoaded
    {
        public int Page { get; set; }
        public int TotalPages { get; set; }
        public List<Movie> NewPage { get; set; }
    }
}
