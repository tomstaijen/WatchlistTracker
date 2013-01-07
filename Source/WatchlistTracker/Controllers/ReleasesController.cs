using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using WatchlistTracker.Extensions;
using WatchlistTracker.Logic;
using WatchlistTracker.Models;

namespace WatchlistTracker.Controllers
{
    public class ReleasesController : BaseApiController
    {
        public ReleasesController(UserRepository userRepository) : base(userRepository)
        {
        }

        [HttpGet]
        [ApiAuthorize]
        public IEnumerable<Release> FindMovieReleases(string name)
        {
            return new PreDB().SearchMovie(name);
        }

        [HttpGet]
        [ApiAuthorize]
        public IEnumerable<TraktMovie> GetWatchlist()
        {
            var trakt = new Trakt(User.TraktUser);
            var watchlist = trakt.GetMoviesWatchlist().ToList();
            var collection = trakt.GetMoviesCollection().Select(c => c.TmdbId);
            watchlist.ToList().ForEach(m =>
                                  {
                                      var found = collection.Any(c => c == m.TmdbId);
                                      m.InCollection = found;
                                  });

            watchlist.Where( m=> m.ReleaseDate < DateTime.Today && !m.InCollection).ForEach(m => m.Releases = new PreDB().SearchMovie(m.Title));
            return watchlist;
        }

        [HttpPost]
        [ApiAuthorize]
        public IEnumerable<ReleasesResult> SearchReleases([FromBody] ReleasesRequest request)
        {
            var predb = new PreDB();
            return request.Titles.Select(t => new ReleasesResult()
                                                  {
                                                      Title = t,
                                                      Releases = predb.SearchMovie(t)
                                                  }
                ).ToList();
        }

        [HttpPost]
        [ApiAuthorize]
        public HttpResponseMessage Seen(TraktMovie movie)
        {
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpPost]
        [ApiAuthorize]
        public HttpResponseMessage AddToCollection(TraktMovie movie)
        {
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpPost]
        [ApiAuthorize]
        public HttpResponseMessage RemoveFromCollection(TraktMovie movie)
        {
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpPost]
        [ApiAuthorize]
        public HttpResponseMessage RemoveFromWatchlist(TraktMovie movie)
        {
            return Request.CreateResponse(HttpStatusCode.OK);
        }


    }
    
    public class MovieWrapper
    {
        public TraktMovie Movie { get; set; }
        public bool InCollection { get; set;}
    }

    public class ReleasesRequest
    {
        public string[] Titles { get; set; }
    }

    public class ReleasesResult
    {
        public string Title { get; set; }
        public IEnumerable<Release> Releases { get; set; }
    }
}