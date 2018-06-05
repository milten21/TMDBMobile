using System;
using Newtonsoft.Json;

namespace TMDBMobile.Core.Model
{
    public class Profile
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Username { get; set; }

        [JsonProperty(PropertyName = "inculde_adult")]
        public bool IncludeAdult { get; set; }
    }
}
