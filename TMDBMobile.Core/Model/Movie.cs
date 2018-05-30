using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace TMDBMobile.Core.Model
{
    public class Movie
    {
        public int Id { get; set; }
        public bool Video { get; set; }
        public bool Adult { get; set; }
        public string Overview { get; set; }
        public string Title { get; set; }
        public double Popularity { get; set; }

        [JsonProperty(PropertyName = "vote_count")]
        public int VoteCount { get; set; }

        [JsonProperty(PropertyName = "vote_average")]
        public double VoteAverage { get; set; }

        [JsonProperty(PropertyName = "poster_path")]
        public string PosterPath { get; set; }

        [JsonProperty(PropertyName = "original_language")]
        public string OriginalLanguage { get; set; }

        [JsonProperty(PropertyName = "original_title")]
        public string OriginalTitle { get; set; }

        [JsonProperty(PropertyName = "genre_ids")]
        public List<int> GenreIds { get; set; }

        [JsonProperty(PropertyName = "backdrop_path")]
        public string BackdropPath { get; set; }

        [JsonProperty(PropertyName = "release_date")]
        public DateTime ReleaseDate { get; set; }
    }
}
