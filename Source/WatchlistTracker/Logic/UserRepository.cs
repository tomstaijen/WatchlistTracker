using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using Raven.Client;
using Raven.Client.Document;
using WatchlistTracker.Models;

namespace WatchlistTracker.Logic
{
    public class UserRepository
    {
        private IDocumentSession _session;
        public UserRepository(IDocumentSession session)
        {
            _session = session;
        }

        public User GetUser(string username)
        {
            return _session.Query<User>().SingleOrDefault(u => u.Username == username);
        }

        public void SaveChanged()
        {
            _session.SaveChanges();
        }

        public void Register(User user)
        {
            if( GetUser(user.Username) != null )
                throw new ArgumentException("User already exists.");
            _session.Store(user);
            _session.SaveChanges();
        }
    }
}