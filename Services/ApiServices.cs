using System;
using RestSharp;
using System.Collections.Generic;
using BoxOffice.Models;
using System.Net.Http;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;

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
            var request = new RestRequest($"movie/top_rated?api_key={_apikey}&adult=false", Method.GET);
            var response = client.Execute<MovieResponse>(request);

            if (response.IsSuccessful)
            {
                return response.Data.Results;
            }
            return new List<BoxOffice.Models.Movie>();
        }
        public List<BoxOffice.Models.Movie> GetDiscover()
        {
            var client = new RestClient(BaseUrl);
            var request = new RestRequest($"discover/movie?api_key={_apikey}&adult=false", Method.GET);
            var response = client.Execute<MovieResponse>(request);
            if (response.IsSuccessful)
            {
                return response.Data.Results;
            }
            return new List<BoxOffice.Models.Movie>();
        }
        public List<BoxOffice.Models.Movie> GetDiscoverTV()
        {
            var client = new RestClient(BaseUrl);
            var request = new RestRequest($"discover/tv?api_key={_apikey}&adult=false", Method.GET);
            var response = client.Execute<TvResponse>(request);
            if (response.IsSuccessful)
            {
                return response.Data.Results;
            }
            return new List<BoxOffice.Models.Movie>();
        }
        public List<CastMember> GetMovieCast(int movieId)
        {
            var client = new RestClient(BaseUrl);
            var request = new RestRequest($"movie/{movieId}/credits?api_key={_apikey}", Method.GET);
            var response = client.Execute<MovieCreditsResponse>(request);

            if (response.IsSuccessful)
            {
                return response.Data.Cast;
            }
            return new List<CastMember>();
        }
        public List<CastMember> GetTvShowCast(int tvShowId)
        {
            var client = new RestClient(BaseUrl);
            var request = new RestRequest($"tv/{tvShowId}/credits?api_key={_apikey}", Method.GET);
            var response = client.Execute<TvShowCreditsResponse>(request);

            if (response.IsSuccessful)
            {
                return response.Data.Cast;
            }
            return new List<CastMember>();
        }
        public List<BoxOffice.Models.TrendingMovie> GetTrendingMovies()
        {
            var client = new RestClient(BaseUrl);
            var request = new RestRequest($"trending/all/week?api_key={_apikey}&adult=false&language=en-US&sort_by=popularity.desc", Method.GET);
            var response = client.Execute<MovieTrendingResponse>(request);

            if (response.IsSuccessful)
            {
                var trendingMovies = response.Data.Results;

                foreach (var trendingMovie in trendingMovies)
                {
                    var movieDetails = GetMovieDetails(trendingMovie.Id);
                    if (movieDetails != null)
                    {
                        trendingMovie.Genres = movieDetails.Genres;
                        trendingMovie.tagline = movieDetails.tagline;
                    }
                    var tvDetails = GetTvDetails(trendingMovie.Id);
                    if (tvDetails != null)
                    {
                        trendingMovie.Genres = tvDetails.Genres;
                        trendingMovie.tagline = tvDetails.tagline;
                    }
                }

                return trendingMovies;
            }
            return new List<BoxOffice.Models.TrendingMovie>();
        }
        public List<Video> GetMovieTrailers(int movieId)
        {
            var client = new RestClient(BaseUrl);
            var request = new RestRequest($"movie/{movieId}/videos?api_key={_apikey}", Method.GET);
            var response = client.Execute<VideoResponse>(request);

            if (response.IsSuccessful)
            {
                return response.Data.Results;
            }
            return new List<Video>();
        }
        public List<Video> GetTVTrailers(int showId)
        {
            var client = new RestClient(BaseUrl);
            var request = new RestRequest($"tv/{showId}/videos?api_key={_apikey}", Method.GET);
            var response = client.Execute<TvVideoResponse>(request);

            if (response.IsSuccessful)
            {
                return response.Data.Results;
            }
            return new List<Video>();
        }
        public Movie GetMovieDetails(int movieId)
        {
            var client = new RestClient(BaseUrl);
            var request = new RestRequest($"movie/{movieId}?api_key={_apikey}", Method.GET);
            var response = client.Execute<Movie>(request);

            if (response.IsSuccessful)
            {
                return response.Data;
            }

            return null;
        }
        public Movie GetTvDetails(int showId)
        {
            var client = new RestClient(BaseUrl);
            var request = new RestRequest($"tv/{showId}?api_key={_apikey}", Method.GET);
            var response = client.Execute<Movie>(request);

            if (response.IsSuccessful)
            {
                return response.Data;
            }

            return null;
        }
    }

    public class VideoResponse
    {
        public List<Video> Results { get; set; }
    }
    public class TvVideoResponse
    {
        public List<Video> Results { get; set; }
    }
    public class MovieCreditsResponse
    {
        public List<CastMember> Cast { get; set; }
    }
    public class TvShowCreditsResponse
    {
        public List<CastMember> Cast { get; set; }
    }
    public class TvResponse
    {
        public List<BoxOffice.Models.Movie> Results { get; set; }
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