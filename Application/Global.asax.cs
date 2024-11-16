using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace Application
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {

        }

        protected void Session_Start(object sender, EventArgs e)
        {
            // lock application state to increment user count
            Application.Lock();

            if (Application["ActiveUsers"] == null)
            {
                Application["ActiveUsers"] = 1;
            }
            else
            {
                Application["ActiveUsers"] = (int)Application["ActiveUsers"] + 1;
            }

            // unlock application state
            Application.UnLock();
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {
            // lock application state to decrement the user count
            Application.Lock();

            // decrement the active user count
            if (Application["ActiveUsers"] != null)
            {
                Application["ActiveUsers"] = (int)Application["ActiveUsers"] - 1;
            }

            // unlock the application state
            Application.UnLock();
        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}