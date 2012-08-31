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
    public partial class Profile : BasePage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            UserName.Text = User.Username;
            
        }

        protected void CreateUserButton_Click(object sender, EventArgs e)
        {
            User.Email = Email.Text;
            User.TraktUser.UserName = TraktUsername.Text;
            User.TraktUser.ApiKey = TraktApiKey.Text;
            User.TraktUser.PasswordHash = TraktPassword.Text;

            try
            {
                //new UserRepository().Register(user);
                //FormsAuthentication.RedirectFromLoginPage(user.Username, true);    
            }
            catch(Exception ex)
            {
                Validators.Add(new CustomValidator { IsValid = false, Text = ex.Message });
                Page.Validate();
            }
        }

        protected void CreateUserButton_Click1(object sender, EventArgs e)
        {

        }

    }
}
