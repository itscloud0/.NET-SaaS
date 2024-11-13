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
            string username = txtUsername.Text;
            string password = txtPassword.Text;

            if (IsStaffValid(username, password))
            {
                Session["StaffUser"] = username;
                Response.Redirect("Staff.aspx");
            }
            else
            {
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
                var doc = XDocument.Load(Server.MapPath("~/Staff.xml"));
                var staff = doc.Descendants("Staff")
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
    }
}