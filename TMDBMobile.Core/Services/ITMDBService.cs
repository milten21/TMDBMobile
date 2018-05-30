using RestSharp;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMDBMobile.Core.Model;

namespace TMDBMobile.Core.Services
{
    public interface ITMDBService
    {
        Task<IRestResponse<MovieSearchResult>> Search(string query, int page = 1, bool includeAdult = false, string language = "en-US");
        Task<IRestResponse<MovieSearchResult>> Discover(int page = 1, bool includeAdult = false, string language = "en-US");
        Task<IRestResponse<List<Genre>>> GetGenres();
    }
}
