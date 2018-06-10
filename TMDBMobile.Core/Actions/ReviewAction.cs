using System;
using System.Collections.Generic;
using TMDBMobile.Core.Model;

namespace TMDBMobile.Core.Actions
{
    public struct StartLoadingReviews
    {
        public int MovieId { get; set; }
    }

    public struct ReviewsLoaded
    {
        public int Page { get; set; }
        public int TotalPages { get; set; }

        public List<MovieReview> Result { get; set; }
    }

    public struct ReviewsLoadError
    {
        public Exception Exception { get; set; }
    }
}
