// Developed by Cole Eastman
// This ASP.NET Web Forms application displays the count of active users on the website.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Application
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        // This event handler is executed whenever the page is loaded.
        protected void Page_Load(object sender, EventArgs e)
        {
            // Retrieve the current number of active users from the Application state.
            // Use the null-coalescing operator (??) to default to 0 if no value exists.
            int activeUsers = (int)(Application["ActiveUsers"] ?? 0);

            // Check if the user is logged in by verifying the Session state.
            if (Session["LoggedInUser"] != null)
            {
                // If the user is logged in, display the active users count.
                lblStatus.Text = $"Active Users: {activeUsers}";
            }
            else
            {
                // If the user is not logged in, display the active users count.
                // This branch duplicates the same action as above, providing uniform behavior.
                lblStatus.Text = $"Active Users: {activeUsers}";
            }
        }
    }
}
