using RestSharp;
using System;
using System.Threading.Tasks;
using TMDBMobile.Core.Model;

namespace TMDBMobile.Core.Services
{
    public class TMDBService : ITMDBService
    {
        public RestClient RestClient { get; protected set; }

        public IConfigurationService ConfigurationService { get; }

        protected string CreateSessionPath => "authentication/session/new";
        protected string CreateTokenRequestPath => "authentication/token/new";
        protected string ValidateTokenPath => "authentication/token/validate_with_login";

        protected string GetAccountDetailsPath => "account";
        // No need to explicitly set accout_id, it will be automatically replaced by API
        protected string GetFavoritesMoviesPath => "account/{account_id}/favorite/movies";
        protected string FavoriteMoviePath => "account/{account_id}/favorite";

        protected string MovieAccountStatePath => "movie/{movie_id}/account_states";
        protected string RateMoviePath => "movie/{movie_id}/rating";
        protected string GetMovieReviewsPath => "movie/{movie_id}/reviews";

        protected string MoviesSearchPath => "search/movie";
        protected string MoviesGenresPath => "genre/movie/list";
        protected string MoviesDiscoverPath => "discover/movie";

        public TMDBService(IConfigurationService configurationService)
        {
            ConfigurationService = configurationService;

            RestClient = new RestClient
            {
                BaseUrl = new Uri(ConfigurationService.ApiUrl)
            };

            RestClient.AddDefaultParameter("api_key", ConfigurationService.ApiKey, ParameterType.QueryString);
        }

        public void SetSessionId(string sessionId)
        {
            RemoveSessionId();
            RestClient.AddDefaultParameter("session_id", sessionId, ParameterType.QueryString);
        }

        public void RemoveSessionId()
        {
            RestClient.RemoveDefaultParameter("session_id");
        }

        public async Task<IRestResponse<RequestTokenResponse>> CreateTokenRequest()
        {
            var request = new RestRequest(CreateTokenRequestPath, Method.GET);

            return await Execute<RequestTokenResponse>(request);
        }

        public async Task<IRestResponse<RequestTokenResponse>> ValidateToken(string username, string password, string requestToken)
        {
            var request = new RestRequest(ValidateTokenPath, Method.GET);

            request.AddParameter("username", username);
            request.AddParameter("password", password);
            request.AddParameter("request_token", requestToken);

            return await Execute<RequestTokenResponse>(request);
        }

        public async Task<IRestResponse<SessionResponse>> CreateSession(string requestToken)
        {
            var request = new RestRequest(CreateSessionPath);

            request.AddParameter("request_token", requestToken);

            return await Execute<SessionResponse>(request);
        }

        public async Task<IRestResponse<MovieSearchResult>> Search(string query, int page = 1, bool includeAdult = false, string language = "en-US")
        {
            var request = new RestRequest(MoviesSearchPath, Method.GET);

            request.AddParameter("query", query, ParameterType.QueryString);
            request.AddParameter("page", page, ParameterType.QueryString);
            request.AddParameter("include_adult", includeAdult, ParameterType.QueryString);
            request.AddParameter("language", language, ParameterType.QueryString);

            return await Execute<MovieSearchResult>(request);
        }

        public async Task<IRestResponse<MovieSearchResult>> Discover(int page = 1, bool includeAdult = false, string language = "en-US")
        {
            var request = new RestRequest(MoviesDiscoverPath, Method.GET);

            request.AddParameter("page", page, ParameterType.QueryString);
            request.AddParameter("include_adult", includeAdult, ParameterType.QueryString);
            request.AddParameter("language", language, ParameterType.QueryString);
            request.AddParameter("sort_by", "popularity.desc", ParameterType.QueryString);

            return await Execute<MovieSearchResult>(request);
        }

        public async Task<IRestResponse<MovieSearchResult>> GetFavoriteMovies(int page = 1, string language = "en-US")
        {
            var request = new RestRequest(GetFavoritesMoviesPath, Method.GET);

            request.AddParameter("page", page, ParameterType.QueryString);
            request.AddParameter("language", language, ParameterType.QueryString);
            request.AddParameter("sort_by", "popularity.desc", ParameterType.QueryString);

            return await Execute<MovieSearchResult>(request);
        }

        public async Task<IRestResponse<GetGenersResponse>> GetGenres()
        {
            return await Execute<GetGenersResponse>(new RestRequest(MoviesGenresPath, Method.GET));
        }

        public async Task<IRestResponse<Profile>> GetProfile()
        {
            return await Execute<Profile>(new RestRequest(GetAccountDetailsPath, Method.GET));
        }

        public async Task<IRestResponse<MovieAccountState>> AccountStateForMovie(int movieId)
        {
            var request = new RestRequest(MovieAccountStatePath, Method.GET);

            request.AddParameter("movie_id", movieId, ParameterType.UrlSegment);

            return await Execute<MovieAccountState>(request);
        }

        public async Task<IRestResponse<TMDBResponse>> FavoriteMovie(bool isFavorite, int movieId) 
        {
            var request = new RestRequest(FavoriteMoviePath, Method.POST);

            request.AddHeader("Content-type", "application/json");

            request.AddJsonBody(new 
            {
                media_type = "movie",
                media_id = movieId,
                favorite = isFavorite
            });

            return await Execute<TMDBResponse>(request);
        }

        public async Task<IRestResponse<TMDBResponse>> RateMovie(int rate, int movieId) 
        {
            var request = new RestRequest(RateMoviePath, Method.POST);

            request.AddParameter("movie_id", movieId, ParameterType.UrlSegment);

            request.AddJsonBody(new 
            {
                value = rate
            });

            return await Execute<TMDBResponse>(request);
        }

        public async Task<IRestResponse<MovieReviewResponse>> GetReviews(int movieId, int page) 
        {
            var request = new RestRequest(GetMovieReviewsPath, Method.GET);

            return await Execute<MovieReviewResponse>(request);
        }

        protected async Task<IRestResponse<TResult>> Execute<TResult>(IRestRequest request)
        {
            try
            {
                return await RestClient.ExecuteTaskAsync<TResult>(request);
            }
            catch (Exception ex)
            {
                return new RestResponse<TResult>
                {
                    ErrorException = ex
                };
            }
        }
    }
}
