using System;

namespace BoxOffice.Models
{
    public class Movie
    {
       public int Id { get; set; }
        public string Title { get; set; }
        public string Overview { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string PosterPath { get; set; }
        public float VoteAverage { get; set; }
    }
    public class TrendingMovie
    {
        public string Title { get; set; }
        public string Overview { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string PosterPath { get; set; }
        public float VoteAverage { get; set; }
    }
}