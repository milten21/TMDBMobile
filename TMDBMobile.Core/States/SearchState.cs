using System;
using System.Collections.Generic;
using TMDBMobile.Core.Model;

namespace TMDBMobile.Core.States
{
    public struct SearchState
    {
        public string Query { get; set; }

        public List<Movie> Movies { get; set; }

        public bool IsSearching { get; set; }

        public int LastLoadedPage { get; set; }
        public int TotalPages { get; set; }

        public Exception Exception { get; set; }
    }
}
