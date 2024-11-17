using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace Application
{
    // Developed by Cole Eastman
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {

        }

        protected void Session_Start(object sender, EventArgs e)
        {
            // Lock application state to increment user count
            Application.Lock();

            // If no active users, initialize to 1
            if (Application["ActiveUsers"] == null)
            {
                Application["ActiveUsers"] = 1;
            }
            else
            {
                // Increment the active user count
                Application["ActiveUsers"] = (int)Application["ActiveUsers"] + 1;
            }

            // Unlock application state
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
            // Lock application state to decrement the user count
            Application.Lock();

            // Decrement the active user count
            if (Application["ActiveUsers"] != null)
            {
                Application["ActiveUsers"] = (int)Application["ActiveUsers"] - 1;
            }

            // Unlock application state
            Application.UnLock();
        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}
