using System;
using System.Collections.Generic;
using TMDBMobile.Core.Model;

namespace TMDBMobile.Core.Actions
{
    public struct StartLoadingFavouritePage { }

    public struct FailedToLoadFavouritePage
    {
        public Exception Exception { get; set; }
    }

    public struct FavouritePageLoaded
    {
        public int Page { get; set; }
        public int TotalPages { get; set; }
        public List<Movie> NewPage { get; set; }
    }
}
