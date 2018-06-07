using System;

namespace TMDBMobile.Core.Model
{
    public class FavoriteMovieArgs
    {
        public int MovieId { get; set; }
        public bool IsFavorite { get; set; }
    }
}