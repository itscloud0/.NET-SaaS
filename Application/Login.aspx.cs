using System;
using System.Linq;
using System.Web.UI;
using System.Xml.Linq;

namespace Application
{
    public partial class Login : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Any additional setup code can go here
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                lblError.Text = "Please enter both a username and password.";
                return;
            }

            if (IsStaffValid(username, password))
            {
                // Valid staff credentials, redirect to Admin page
                Session["StaffUser"] = username;
                Response.Redirect("Admin.aspx");
            }
            else if (IsMemberValid(username, password))
            {
                // Valid member credentials, redirect to Member page
                Session["MemberUser"] = username;
                Response.Redirect("MemberPage.aspx"); // Replace with the actual member page
            }
            else
            {
                // Invalid credentials for both staff and member
                lblError.Text = "Invalid username or password.";
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }

        private bool IsStaffValid(string username, string password)
        {
            try
            {
                // Load Staff.xml and check for matching credentials
                var doc = XDocument.Load(Server.MapPath("~/Staff.xml"));
                var staff = doc.Descendants("Staff") // Matching <Staff> element
                               .FirstOrDefault(s => (string)s.Element("Username") == username &&
                                                    (string)s.Element("Password") == password);
                return staff != null;
            }
            catch (Exception ex)
            {
                lblError.Text = "Error reading staff credentials: " + ex.Message;
                return false;
            }
        }

        private bool IsMemberValid(string username, string password)
        {
            try
            {
                // Load Member.xml and check for matching credentials
                var doc = XDocument.Load(Server.MapPath("~/Member.xml"));
                var member = doc.Descendants("Members") // Matching <Members> element
                                .FirstOrDefault(m => (string)m.Element("Username") == username &&
                                                     (string)m.Element("Password") == password);
                return member != null;
            }
            catch (Exception ex)
            {
                lblError.Text = "Error reading member credentials: " + ex.Message;
                return false;
            }
        }
    }
}