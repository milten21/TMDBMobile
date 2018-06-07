using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace TMDBMobile.Core.Model
{
    public class MovieReviewResponse : TMDBResponse
    {
        public int Id { get; set; }
        public int Page { get; set; }
        public List<MovieReview> Results { get; set; }

        [JsonProperty("total_pages")]
        public int TotalPages { get; set; }

        [JsonProperty("total_results")]
        public int TotalResults { get; set; }
    }
}
