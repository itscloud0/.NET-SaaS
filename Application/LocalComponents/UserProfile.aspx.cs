using System;
using System.Web;

namespace Application
{
    // UserProfile page class developed by Chris Harris
    public partial class UserProfile : System.Web.UI.Page
    {
        // Page load event to check for existing profile data
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) // Only load profile if it's not a postback
            {
                LoadProfileFromCookie(); // Load profile from cookie on first load
            }
        }

        // Method to load the user profile data from the cookie
        private void LoadProfileFromCookie()
        {
            // Check if the "UserProfile" cookie exists
            if (Request.Cookies["UserProfile"] != null)
            {
                HttpCookie userCookie = Request.Cookies["UserProfile"];

                // Retrieve and display profile data from the cookie
                txtName.Text = userCookie["Name"];
                txtAge.Text = userCookie["Age"];

                // Also load the profile into the session for easy access across pages
                Session["UserName"] = userCookie["Name"];
                Session["UserAge"] = userCookie["Age"];
            }
        }

        // Button click event to save the user profile
        protected void btnSaveProfile_Click(object sender, EventArgs e)
        {
            string name = txtName.Text.Trim(); // Get the trimmed username from the input field
            string age = txtAge.Text.Trim();   // Get the trimmed age from the input field

            // Check if both fields are filled
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(age))
            {
                lblMessage.Text = "Please fill in both fields."; // Show an error message if any field is empty
                return;
            }

            // Save the profile data into a cookie
            HttpCookie userCookie = new HttpCookie("UserProfile");
            userCookie["Name"] = name;
            userCookie["Age"] = age;
            userCookie.Expires = DateTime.Now.AddMinutes(30); // Set cookie expiration to 30 minutes
            Response.Cookies.Add(userCookie); // Add the cookie to the response

            // Save the profile data into the session
            Session["UserName"] = name;
            Session["UserAge"] = age;

            // Display success message
            lblMessage.Text = "Profile saved successfully!";
        }

        // Button click event to show the user profile from the session
        protected void btnShowProfile_Click(object sender, EventArgs e)
        {
            // Retrieve the profile data from the session
            string userName = Session["UserName"] as string;
            string userAge = Session["UserAge"] as string;

            // Check if session data is available
            if (!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(userAge))
            {
                // Display the session data (name and age)
                lblMessage.Text = $"Session Info: Name - {userName}, Age - {userAge}";
            }
            else
            {
                // Display message if no session data is found
                lblMessage.Text = "No session data available. Save your profile first.";
            }
        }
    }
}


