using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace BoxOffice.Models
{

    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string name { get; set; }
        public string tagline { get; set; }
        public string Overview { get; set; }
        public DateTime ReleaseDate { get; set; }
        public DateTime First_air_date { get; set; }
        public string poster_path { get; set; }
        public float vote_average { get; set; }
        public string backdrop_path { get; set; }
        public string media_type { get; set; }
        public List<CastMember> Cast { get; set; }
        public int Budget { get; internal set; }
        public int revenue { get; set; }
        public int runtime { get; set; }
        
        [JsonProperty("genres")]
        public List<Genre> Genres { get; set; }
        public List<Video> Videos { get; internal set; }
    }
    public class TrendingMovie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string name { get; set; }
        public string Overview { get; set; }
        public DateTime ReleaseDate { get; set; }
        public DateTime First_air_date { get; set; }
        public string poster_path { get; set; }
        public float vote_average { get; set; }
        public string backdrop_path { get; set; }
        public string tagline { get; set; }
        public string media_type { get; set; }
        public int Budget { get; set; }
        public int revenue { get; set; }
        public int runtime { get; set; }
        public List<CastMember> Cast { get; set; }
        
        [JsonProperty("genres")]
        public List<Genre> Genres { get; set; }

        [JsonProperty("videos")] 
        public List<Video> Videos { get; set; }
    }
        public class SearchResult
        {
            public List<Movie> Results { get; set; }
    }
    public class CastMember
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Character { get; set; }
            public string ProfilePath { get; set; }
        }

    public class Genre
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class Video
    {
        public string Key { get; set; }
        public string Name { get; set; }
        public string Site { get; set; }
        public int Size { get; set; }
        public string Type { get; set; }
    }
}