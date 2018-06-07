using System;
namespace TMDBMobile.Core.Model
{
    public class MovieAccountState
    {
        public int Id { get; set; }
        public bool Favorite { get; set; }
        public object Rated { get; set; }
        public bool Watchlist { get; set; }

    }
}
