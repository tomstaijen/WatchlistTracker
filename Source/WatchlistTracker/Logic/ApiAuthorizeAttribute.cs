using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;

namespace WatchlistTracker.Logic
{
    public class ApiAuthorize : System.Web.Http.AuthorizeAttribute
    {
        public override void OnAuthorization(
               System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            base.OnAuthorization(actionContext);
            if( !HttpContext.Current.User.Identity.IsAuthenticated )
            {
                actionContext.Request.CreateResponse(HttpStatusCode.Forbidden, "Not authenticated.");
            }
        }
    }
}