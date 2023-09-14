using System;
using RestSharp;
using System.Collections.Generic;

namespace BoxOffice.Services
{
    public class ApiServices
    {
        private const string BaseUrl = "https://api.themoviedb.org/3";
        private readonly string _apikey;

        public ApiServices(string apikey)
        {
            _apikey = apikey;
        }

        public List<BoxOffice.Models.Movie> GetTopRatedMovies()
        {
            var client = new RestClient(BaseUrl);
            var request = new RestRequest($"movie/top_rated?api_key={_apikey}", Method.GET);
            var response = client.Execute<MovieResponse>(request);

            if (response.IsSuccessful)
            {
                return response.Data.Results;
            }
            return new List<BoxOffice.Models.Movie>();
        }
        public List<BoxOffice.Models.TrendingMovie> GetTrendingMovies()
        {
            var client = new RestClient(BaseUrl);
            var request = new RestRequest($"trending/movie/week?api_key={_apikey}", Method.GET);
            var response = client.Execute<MovieTrendingResponse>(request);

            if (response.IsSuccessful)
            {
                return response.Data.Results;
            }
            return new List<BoxOffice.Models.TrendingMovie>();
        }
    }
    public class MovieResponse
    {
        public List<BoxOffice.Models.Movie> Results { get; set; }
    }
    public class MovieTrendingResponse
    {
        public List<BoxOffice.Models.TrendingMovie> Results { get; set; }
    }
}