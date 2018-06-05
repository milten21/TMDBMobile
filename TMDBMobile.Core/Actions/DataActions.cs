using System.Collections.Generic;
using TMDBMobile.Core.Model;

namespace TMDBMobile.Core.Actions
{
    public struct GenresLoaded
    {
        public List<Genre> Genres { get; set; }
    }
}
