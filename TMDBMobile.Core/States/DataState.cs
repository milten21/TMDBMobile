using System;
using System.Collections.Generic;
using TMDBMobile.Core.Model;

namespace TMDBMobile.Core.States
{
    public struct DataState
    {
        public List<Genre> Genres { get; set; }

        public bool IsLoadingProfile { get; set; }
        public Profile Profile { get; set; }
        public Exception ProfileLoadError { get; set; }

    }
}
