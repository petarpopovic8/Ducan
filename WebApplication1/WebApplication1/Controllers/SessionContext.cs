using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace WebApplication1.Controllers
{
    public class SessionContext
    {
        public void SetAuthenticationToken(string name, bool isPersistant, user userData)
        {
            string data = null;
            if (userData != null)
                data = new JavaScriptSerializer().Serialize(new SimpleUser(userData));

            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, name, DateTime.Now, DateTime.Now.AddHours(10), isPersistant, userData.full_name);

            string cookieData = FormsAuthentication.Encrypt(ticket);
            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, cookieData)
            {
                HttpOnly = true,
                Expires = ticket.Expiration
            };

            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        public SimpleUser GetUserData()
        {
            SimpleUser userData = null;

            try
            {
                HttpCookie cookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
                if (cookie != null)
                {
                    FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(cookie.Value);

                    userData = new JavaScriptSerializer().Deserialize(ticket.UserData, typeof(SimpleUser)) as SimpleUser;
                }
            }
            catch (Exception ex)
            {
            }

            return userData;
        }

        public class SimpleUser
        {
            public string username { get; set; }
            public int id { get; set; }

            public SimpleUser(user user)
            {
                id = user.id;
                username = user.full_name;
            }
        }
    }
}