using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WatchlistTracker.Logic;

namespace WatchlistTracker.Models
{
    public class User
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public TraktUser TraktUser { get; set; }
    }
}