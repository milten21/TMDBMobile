using System.Collections.Generic;

namespace TMDBMobile.Core.Model
{
    public class GetGenersResponse : TMDBResponse
    {
        public List<Genre> Genres { get; set; }
    }
}
