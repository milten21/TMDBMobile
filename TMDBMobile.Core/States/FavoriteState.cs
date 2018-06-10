using System;
using System.Collections.Generic;
using TMDBMobile.Core.Model;

namespace TMDBMobile.Core.States
{
    public struct FavoriteState
    {
        public List<Movie> Movies { get; set; }

        public int LastLoadedPage { get; set; }
        public int TotalPages { get; set; }

        public bool IsLoadingPage { get; set; }
        public Exception Exception { get; set; }
    }
}
