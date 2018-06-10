using System;
using System.Collections.Generic;
using System.Text;
using TMDBMobile.Core.Model;

namespace TMDBMobile.Core.States
{
    public struct ReviewsState
    {
        public bool IsLoadingReviews { get; set; }

        public int MovieId { get; set; }

        public int LastLoadedPage { get; set; }
        public int TotalPages { get; set; }

        public List<MovieReview> Reviews { get; set; }

        public Exception Error { get; set; }
    }
}
