using Newtonsoft.Json;

namespace TMDBMobile.Core.Model
{
    public class SessionResponse : TMDBResponse
    {
        // Will not be returned in case of 
        public bool Success { get; set; }

        [JsonProperty(PropertyName = "session_id")]
        public string SessionId { get; set; }
    }
}
