using RestSharp;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMDBMobile.Core.Model;

namespace TMDBMobile.Core.Services
{
    public interface ITMDBService
    {
        void SetSessionId(string sessionId);
        void RemoveSessionId();

        Task<IRestResponse<RequestTokenResponse>> CreateTokenRequest();
        Task<IRestResponse<SessionResponse>> CreateSession(string requestToken);
        Task<IRestResponse<RequestTokenResponse>> ValidateToken(string username, string password, string requestToken);

        Task<IRestResponse<MovieSearchResult>> Search(string query, int page = 1, bool includeAdult = false, string language = "en-US");
        Task<IRestResponse<MovieSearchResult>> Discover(int page = 1, bool includeAdult = false, string language = "en-US");
        Task<IRestResponse<MovieSearchResult>> GetFavoriteMovies(int page = 1, string language = "en-US");

        Task<IRestResponse<List<Genre>>> GetGenres();
    }
}
