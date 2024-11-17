using System;
using System.Web.UI;

namespace Application
{
    // Developed by Ilia Sorokin, Chris Harris
    public partial class Default : Page
    {
        // Page_Load method, triggers when the page is loaded
        protected void Page_Load(object sender, EventArgs e)
        {
            // Check if this is the first time the page is loaded
            if (!IsPostBack)
            {
                UpdateLoginStatus(); // Update login status display
            }
        }

        // Method to update the login status message based on session data
        private void UpdateLoginStatus()
        {
            // Check if a regular user is logged in
            if (Session["LoggedInUser"] != null)
            {
                lblStatus.Text = $"Logged in as: {Session["LoggedInUser"]}"; // Display logged in username
            }
            // Check if a staff user is logged in
            else if (Session["StaffUser"] != null)
            {
                lblStatus.Text = $"Logged in as Staff: {Session["StaffUser"]}"; // Display logged in staff username
            }
            // If no user is logged in
            else
            {
                lblStatus.Text = "Not logged in"; // Display not logged in message
            }
        }

        // Redirect to the Member page if the user is logged in
        protected void GoToMemberPage(object sender, EventArgs e)
        {
            // If the regular user is not logged in, redirect to Login page
            if (Session["LoggedInUser"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            else
            {
                // Redirect to Member page
                Response.Redirect("Member.aspx");
            }
        }

        // Redirect to the Admin page if the staff user is logged in
        protected void GoToStaffPage(object sender, EventArgs e)
        {
            // If the staff user is not logged in, redirect to Login page
            if (Session["StaffUser"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            else
            {
                // Redirect to Admin page
                Response.Redirect("Admin.aspx");
            }
        }
    }
}
