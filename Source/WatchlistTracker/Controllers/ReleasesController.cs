using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace WatchlistTracker.Controllers
{
    public class ReleasesController : ApiController
    {
        [HttpPost]
        public IEnumerable<string> Find(string id)
        {
            return new string[] { "Hallo"};
        }
    }
}