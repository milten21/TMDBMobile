using Newtonsoft.Json;
using System;

namespace TMDBMobile.Core.Model
{
    public class RequestTokenResponse : TMDBResponse
    {
        // probably will be false, when api_key is invalid
        public bool Success { get; set; }

        [JsonProperty(PropertyName = "expires_at")]
        public DateTime ExpiresAt { get; set; }

        [JsonProperty(PropertyName = "request_token")]
        public string RequestToken { get; set; }
    }
}
