using System;
using System.Web;

namespace Application
{
    public partial class UserProfile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadProfileFromCookie();
            }
        }

        private void LoadProfileFromCookie()
        {
            // Check if the cookie exists
            if (Request.Cookies["UserProfile"] != null)
            {
                HttpCookie userCookie = Request.Cookies["UserProfile"];
                txtName.Text = userCookie["Name"];
                txtAge.Text = userCookie["Age"];

                // Also load the profile into the session for sharing
                Session["UserName"] = userCookie["Name"];
                Session["UserAge"] = userCookie["Age"];
            }
        }

        protected void btnSaveProfile_Click(object sender, EventArgs e)
        {
            string name = txtName.Text.Trim();
            string age = txtAge.Text.Trim();

            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(age))
            {
                lblMessage.Text = "Please fill in both fields.";
                return;
            }

            // Save the profile to a cookie
            HttpCookie userCookie = new HttpCookie("UserProfile");
            userCookie["Name"] = name;
            userCookie["Age"] = age;
            userCookie.Expires = DateTime.Now.AddMinutes(30); // Cookie expires in 30 minutes
            Response.Cookies.Add(userCookie);

            // Save the profile to the session
            Session["UserName"] = name;
            Session["UserAge"] = age;

            lblMessage.Text = "Profile saved successfully!";
        }

        protected void btnShowProfile_Click(object sender, EventArgs e)
        {
            // Retrieve data from the session
            string userName = Session["UserName"] as string;
            string userAge = Session["UserAge"] as string;

            if (!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(userAge))
            {
                lblMessage.Text = $"Session Info: Name - {userName}, Age - {userAge}";
            }
            else
            {
                lblMessage.Text = "No session data available. Save your profile first.";
            }
        }
    }
}