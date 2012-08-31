
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace WatchlistTracker.Models
{
    public class Movie
    {
        public string Title { get; set; }
        public DateTime Released { get; set; }
        public short Year { get; set; }
        public string ImdbId { get; set; }
        public short Runtime { get; set; }
        public string Tagline { get; set; }
        public string[] Genres { get; set; }
        public string Poster { get; set; }
    }

    public class TraktMovie
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("year")]
        public short Year { get; set; }

        [JsonProperty("released")]
        public long Released { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("trailer")]
        public string Trailer { get; set; }

        [JsonProperty("runtime")]
        public short Runtime { get; set; }

        [JsonProperty("tagline")]
        public string Tagline { get; set; }

        [JsonProperty("overview")]
        public string Overview { get; set; }

        [JsonProperty("certification")]
        public string Certification { get; set; }

        [JsonProperty("imdb_id")]
        public string ImdbId { get; set; }

        [JsonProperty("tmdb_id")]
        public string TmdbId { get; set; }

        [JsonProperty("inserted")]
        public long Interted { get; set; }

        [JsonProperty("images")]
        public Images Images { get; set; }

        [JsonProperty("genres")]
        public string[] Genres { get; set; }

        [JsonProperty("ratings")]
        public Ratings Ratings { get; set; }

        [JsonProperty("ReleaseDate")]
        public DateTime? ReleaseDate
        {
            get { return new DateTime(Released*10000000).AddYears(1969).AddDays(-1).Date; }
        }

        [JsonProperty("InCollection")]
        public bool InCollection { get; set; }
    }

    public class Ratings
    {
        [JsonProperty("percentage")]
        public short Percentage { get; set; }
        
        [JsonProperty("votes")]
        public int Votes { get; set; }

        [JsonProperty("loved")]
        public int Loved { get; set; }

        [JsonProperty("hated")]
        public int Hated { get; set; }
    }

    public class Images
    {
        [JsonProperty("poster")]
        public string Poster { get; set; }

        [JsonProperty("fanart")]
        public string FanArt { get; set; }
    }
}