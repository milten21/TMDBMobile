using System.Threading;
using TMDBMobile.Core.Model;

namespace TMDBMobile.Core.States
{
    public struct MovieDetailsState
    {
        public bool IsLoading { get; set; }

        public CancellationTokenSource CancellationToken { get; set; }

        public int MovieId { get; set; }
        public MovieAccountState MovieAccountState { get; set; }
    }
}
