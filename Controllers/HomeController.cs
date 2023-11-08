using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using BoxOffice.Services;
using Newtonsoft.Json;
using System.Configuration;
using BoxOffice.Models;

namespace BoxOffice.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApiServices apiServices;
        private readonly string apiKey = ConfigurationManager.AppSettings["TMDbApiKey"];
        private readonly string searchMovieUrl = "https://api.themoviedb.org/3/search/movie";
        private readonly string searchTvUrl = "https://api.themoviedb.org/3/search/tv";
        private string baseUrl;
        public HomeController()
        {
            apiServices = new ApiServices(apiKey);
        }
        public ActionResult Index()
        {
            var trendingMovies = apiServices.GetTrendingMovies();
            foreach (var movie in trendingMovies)
            {
                var current = movie;
                Parallel.Invoke(
                 () => { current.Cast = apiServices.GetMovieCast(current.Id); },
                 () => { current.Cast = apiServices.GetTvShowCast(current.Id); },
                 () => { current.Videos = apiServices.GetMovieTrailers(current.Id); },
                 () => { current.Videos = apiServices.GetTVTrailers(current.Id); }
             );

                var moviedetails = apiServices.GetMovieDetails(current.Id);
                if (moviedetails != null)
                {
                    current.Budget = moviedetails.Budget;
                    current.tagline = moviedetails.tagline;
                    current.runtime = moviedetails.runtime;
                    current.revenue = moviedetails.revenue;
                }

            }

            return View(trendingMovies);
        }
        public ActionResult Movies()
        {
            var discovermovies = apiServices.GetDiscover();
            foreach(var movies in discovermovies)
            {
                var current = movies;
                Parallel.Invoke(
                () => { current.Cast = apiServices.GetMovieCast(current.Id); },
                () => { current.Videos = apiServices.GetMovieTrailers(current.Id); }
            );
                var moviedetails = apiServices.GetMovieDetails(current.Id);
                if (moviedetails != null)
                {
                    current.Budget = moviedetails.Budget;
                    current.tagline = moviedetails.tagline;
                    current.runtime = moviedetails.runtime;
                    current.revenue = moviedetails.revenue;
                }
            }
            return View(discovermovies);
        }
        public ActionResult TvSeries()
        {
            var discoverTv = apiServices.GetDiscoverTV();
            foreach (var tv in discoverTv)
            {
                var current = tv;
                Parallel.Invoke(
                () => { current.Cast = apiServices.GetTvShowCast(current.Id); },
                () => { current.Videos = apiServices.GetTVTrailers(current.Id); }
            );
            }
                return View(discoverTv);
        }
        public ActionResult Search()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Search(string searchTerm, string searchType)
        {
            using (var client = new HttpClient())
            {  
                if(searchType == "movie")
                {
                    baseUrl = searchMovieUrl;
                }
                else if(searchType == "tv")
                {
                    baseUrl = searchTvUrl;
                }
                var url = $"{baseUrl}?api_key={apiKey}&query={searchTerm}&language=en-US";
                var response = await client.GetStringAsync(url);

                // Parse response and pass data to the view
                var searchResults = JsonConvert.DeserializeObject<SearchResult>(response);

                ViewBag.SearchResults = searchResults.Results;
                ViewBag.SearchType = searchType;
                return View("Search");
            }
        }
    }
}