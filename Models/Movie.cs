using System;
using System.Collections.Generic;

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
        public int Id { get; set; }
        public string Title { get; set; }
        public string name { get; set; }
        public string Overview { get; set; }
        public DateTime ReleaseDate { get; set; }
        public DateTime First_air_date { get; set; }
        public string PosterPath { get; set; }
        public float VoteAverage { get; set; }
        public string backdrop_path { get; set; }
        public string media_type { get; set; }
        public List<CastMember> Cast { get; set; }
    }
    public class CastMember
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Order { get; set; }
        public string ProfilePath { get; set; }
    }
}