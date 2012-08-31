using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Security;
using Newtonsoft.Json;
using WatchlistTracker.Logic;
using WatchlistTracker.Models;

namespace WatchlistTracker.Controllers
{
    public class UserController : BaseApiController
    {
        public UserController(UserRepository userRepository) : base(userRepository)
        {
        }
     
        [HttpGet]
        [ApiAuthorize]
        public TraktUser GetCurrentUser()
        {
            if (User == null)
                return null;
            return User.TraktUser;
        }

        [HttpPost]
        public HttpResponseMessage Login([FromBody] LoginRequest request)
        {
            var user = _userRepository.GetUser(request.Username);
            if (user == null)
                return Request.CreateResponse(HttpStatusCode.OK, new {Success = false, Reason = "Unknown user."});
            if( user.Password != request.Password )
                return Request.CreateResponse(HttpStatusCode.OK, new { Success = false, Reason = "Invalid password." });
            
            // login success
            FormsAuthentication.SetAuthCookie(request.Username, true);
            return Request.CreateResponse(HttpStatusCode.OK, new { Success = true, Reason = "", Redirect = FormsAuthentication.GetRedirectUrl(request.Username, true) });
        }

        [HttpPost]
        public bool IsValidTraktUser(ValidateRequest request)
        {
            return new Trakt(null).Validate(request.ApiKey, request.Login);
        }
    }


    public class ValidateRequest
    {
        public string ApiKey { get; set; }
        public TraktLogin Login { get; set; }
    }

    public class TraktLogin
    {
        [JsonProperty("username")]
        public string Username { get; set; }

        /// <summary>
        /// SHA1 hash of the password
        /// </summary>
        [JsonProperty("password")]
        public string Password { get; set; }
    }

    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}