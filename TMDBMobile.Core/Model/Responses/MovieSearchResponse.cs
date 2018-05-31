using Newtonsoft.Json;
using System.Collections.Generic;

namespace TMDBMobile.Core.Model
{
    public class MovieSearchResult : TMDBResponse
    {
        [JsonProperty(PropertyName = "total_results")]
        public int TotalResults { get; set; }

        [JsonProperty(PropertyName = "total_pages")]
        public int TotalPages { get; set; }

        public int Page { get; set; }
        public List<Movie> Results { get; set; }
    }
}
