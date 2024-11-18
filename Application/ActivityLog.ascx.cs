using System;
using System.Web.UI.WebControls;

namespace Application
{
    //Developed by Ilia Sorokin
    public partial class ActivityLog : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadActivityLog();
            }
        }

        // Add a general activity log entry
        public void AddActivity(string activity)
        {
            string logEntry = $"{DateTime.Now}: {activity}";
            lstActivityLog.Items.Insert(0, logEntry); // Insert at the top for the most recent activities
            SaveActivityLog(); // Save log to session after every new entry
        }

        // Track the flight booking activity
        public void LogFlightBooking(string time, string depart, string arrival)
        {
            string activity = $"Booked a flight at {time} from {depart} to {arrival}";
            AddActivity(activity);
        }

        // Track the hotel booking activity
        public void LogHotelBooking( string hotelName, string checkInDate, string checkOutDate)
        {
            string activity = $"Booked a hotel: {hotelName} from {checkInDate} to {checkOutDate}";
            AddActivity(activity);
        }

        // Track login activity
        public void LogLogin(string userName)
        {
            string activity = $"Logged in as {userName}";
            AddActivity(activity);
        }

        // Track Logout activity
        public void LogLogout()
        {
            string activity = "Logged out.";
            AddActivity(activity);
        }

        // Load the activity log from session (or other persistent storage)
        private void LoadActivityLog()
        {
            if (Session["ActivityLog"] != null)
            {
                lstActivityLog.Items.Clear();
                var activityLog = (string[])Session["ActivityLog"];
                foreach (var log in activityLog)
                {
                    lstActivityLog.Items.Add(log);
                }
            }
        }

        // Save the activity log to session for persistence across postbacks
        private void SaveActivityLog()
        {
            var activityLog = new string[lstActivityLog.Items.Count];
            for (int i = 0; i < lstActivityLog.Items.Count; i++)
            {
                activityLog[i] = lstActivityLog.Items[i].Text;
            }
            Session["ActivityLog"] = activityLog;
        }

        // Override OnUnload to save the log
        protected override void OnUnload(EventArgs e)
        {
            base.OnUnload(e);
            SaveActivityLog(); // Save log to session on page unload
        }

        // Method to clear the activity log
        private void ClearLog()
        {
            lstActivityLog.Items.Clear(); // Clears the ListBox displaying the activity log
            Session["ActivityLog"] = new string[0]; // Clear the session data
        }

        protected void ClearLog_Click(object sender, EventArgs e)
        {
            ClearLog();
        }
    }
}
