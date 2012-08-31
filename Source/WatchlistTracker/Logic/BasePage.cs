using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using WatchlistTracker.Models;

namespace WatchlistTracker.Logic
{
    public class BasePage : Page
    {
        public UserRepository UserRepository { get; set; }

        private User _user;
        public User User
        {
            get
            {
                if( _user == null )
                {
                    _user = UserRepository.GetUser(Context.User.Identity.Name);
                }
                return _user;
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if( !this.Context.User.Identity.IsAuthenticated )
                FormsAuthentication.RedirectToLoginPage();
        }
    }
}