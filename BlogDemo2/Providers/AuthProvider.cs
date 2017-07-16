using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace BlogDemo2.Providers
{
    public class AuthProvider : IAuthProvider
    {
        public bool IsLoggedIn => HttpContext.Current.User.Identity.IsAuthenticated;

        public bool Login(string username, string password)
        {
            var result = FormsAuthentication.Authenticate(username, password);

            if (result)
                FormsAuthentication.SetAuthCookie(username, false);

            return result;
        }

        public void Logout()
        {
            FormsAuthentication.SignOut();
        }
    }
}