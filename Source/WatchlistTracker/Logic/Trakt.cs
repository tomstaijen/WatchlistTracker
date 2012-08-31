using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WatchlistTracker.Controllers;
using WatchlistTracker.Models;

namespace WatchlistTracker.Logic
{
    public class Trakt
    {
        private ITraktUser _user;

        public Trakt(ITraktUser user)
        {
            _user = user;
        }

        public bool Validate(string apiKey, TraktLogin login)
        {
            var result = HttpHelper.PostJsonReadObject(string.Format("http://api.trakt.tv/account/test/{0}", apiKey),
                                                       login);
            return result != null && (string)result["status"] == "success";
        }

        public IEnumerable<TraktMovie> GetMoviesWatchlist()
        {
            var json =
                HttpHelper.ReadJsonArray(string.Format("http://api.trakt.tv/user/watchlist/movies.json/{0}/{1}", _user.ApiKey,
                                                  _user.UserName));

            var serializer = new JsonSerializer();
            return json.Select(j => (TraktMovie)serializer.Deserialize(new JTokenReader(j), typeof(TraktMovie)));
        }

        public IEnumerable<TraktMovie> GetMoviesCollection()
        {
            var json = HttpHelper.ReadJsonArray(string.Format("http://api.trakt.tv/user/library/movies/collection.json/{0}/{1}", _user.ApiKey,
                                      _user.UserName));
            var serializer = new JsonSerializer();
            return json.Select(j => (TraktMovie)serializer.Deserialize(new JTokenReader(j), typeof(TraktMovie)));
        }
    }

    public interface ITraktUser
    {
        string ApiKey { get; set; }
        string UserName { get; set; }
        string PasswordHash { get; set; }
    }

    public class TraktUser : ITraktUser
    {
        public string ApiKey { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
    }
}