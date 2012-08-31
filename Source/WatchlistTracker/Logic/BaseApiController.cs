using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using WatchlistTracker.Models;

namespace WatchlistTracker.Logic
{
    public abstract class BaseApiController : ApiController
    {
        protected UserRepository _userRepository;
        protected BaseApiController(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        private User _user;
        protected User User
        {
            get
            {
                if (_user == null)
                    _user = _userRepository.GetUser(HttpContext.Current.User.Identity.Name);
                return _user;
            }
        }

    }
}