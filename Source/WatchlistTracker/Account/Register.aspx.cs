using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using WatchlistTracker.Logic;
using WatchlistTracker.Models;

namespace WatchlistTracker.Account
{
    public partial class Register : System.Web.UI.Page
    {
        public UserRepository UserRepository { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void CreateUserButton_Click(object sender, EventArgs e)
        {
            var user = new User
                           {
                               Username = UserName.Text,
                               Password = Password.Text,
                               Email = Email.Text,
                               TraktUser = new TraktUser
                                               {
                                                   UserName = TraktUsername.Text,
                                                   ApiKey = TraktApiKey.Text
                                               }
                           };

            try
            {
                UserRepository.Register(user);
                FormsAuthentication.RedirectFromLoginPage(user.Username, true);    
            }
            catch(Exception ex)
            {
                Validators.Add(new CustomValidator { IsValid = false, Text = ex.Message });
                Page.Validate();
            }
        }

    }
}
