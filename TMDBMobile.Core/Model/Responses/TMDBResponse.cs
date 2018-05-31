using Newtonsoft.Json;

namespace TMDBMobile.Core.Model
{
    public class TMDBResponse
    {
        [JsonProperty(PropertyName = "status_code")]
        public int StatusCode { get; set; }

        [JsonProperty(PropertyName = "status_message")]
        public string StatusMessage { get; set; }
    }
}
