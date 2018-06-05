using System;
using System.Collections.Generic;
using TMDBMobile.Core.Model;

namespace TMDBMobile.Core.Actions
{
    public struct GenresLoaded
    {
        public List<Genre> Genres { get; set; }
    }

    public struct StartLoadingProfile { }

    public struct ProfileLoaded
    {
        public Profile Profile { get; set; }
    }

    public struct FailedLoadProfile
    {
        public Exception Exception { get; set; }
    }
}
