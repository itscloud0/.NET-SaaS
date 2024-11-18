using System;
using System.Web.UI.WebControls;

namespace Application
{
    // Developed by Ilia Sorokin
    public partial class ActivityLogPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Ensure the activity log is loaded when the page is first accessed.
            if (!IsPostBack)
            {
                LoadActivityLog();
            }
        }

        // Load the activity log from the session
        private void LoadActivityLog()
        {
            if (Session["ActivityLog"] != null)
            {
                lstActivityLog.Items.Clear(); // Clear the current list
                var activityLog = (string[])Session["ActivityLog"]; // Retrieve the log from session
                foreach (var log in activityLog)
                {
                    lstActivityLog.Items.Add(log); // Add each log item to the ListBox
                }
            }
        }

        // Handle the 'Clear Log' button click event
        protected void ClearLog_Click(object sender, EventArgs e)
        {
            lstActivityLog.Items.Clear(); // Clears the ListBox displaying the activity log
            Session["ActivityLog"] = new string[0]; // Clear the session data to reset the log
            lblStatus.Text = "Log has been cleared."; // Provide a status message to the user
        }
    }
}
