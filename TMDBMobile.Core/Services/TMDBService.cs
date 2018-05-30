using RestSharp;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMDBMobile.Core.Model;

namespace TMDBMobile.Core.Services
{
    public class TMDBService : ITMDBService
    {
        public RestClient RestClient { get; protected set; }

        public IConfigurationService ConfigurationService { get; }

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

        public async Task<IRestResponse<List<Genre>>> GetGenres()
        {
            return await Execute<List<Genre>>(new RestRequest(MoviesGenresPath, Method.GET));
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
