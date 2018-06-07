using System.Threading;
using TMDBMobile.Core.Model;

namespace TMDBMobile.Core.Actions
{
    public struct StartLoadingMovieDetailts
    {
        public int MovieId { get; set; }
        public CancellationTokenSource CancellationToken { get; set; }
    }

    public struct MovieAccountStateLoaded 
    {
        public MovieAccountState AccountState { get; set; }
    }

    public struct UpdateAccountState
    {
        public bool? IsFavorite { get; set; }
        public int? Rate { get; set; }
    }
}
