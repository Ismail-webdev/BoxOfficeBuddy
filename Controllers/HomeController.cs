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

namespace BoxOffice.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApiServices apiServices;
        private readonly string apiKey = ConfigurationManager.AppSettings["TMDbApiKey"];

        public HomeController()
        {
            apiServices = new ApiServices(apiKey);
        }
        public ActionResult Index()
        {
            var trendingMovies = apiServices.GetTrendingMovies();
            foreach (var movie in trendingMovies)
            {
                movie.Cast = apiServices.GetMovieCast(movie.Id);
            }
            return View(trendingMovies);
        }
        public ActionResult About()
        {
            var toprated = apiServices.GetTopRatedMovies();

            return View(toprated);
        }
        public ActionResult Search()
        {
           
            ViewBag.Message = "Your application description page.";
           return View();
        }
    }
}