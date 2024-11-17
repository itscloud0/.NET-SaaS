using System;
using System.Linq;
using System.Web.UI;
using System.Xml.Linq;
using EncryptionDecryption;

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
                Response.Redirect("MemberPage.aspx");
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
                var encryptedUsername = EncDec.Encrypt(username);
                var encryptedPassword = EncDec.Encrypt(password);

                var staff = doc.Descendants("Staff")
                    .FirstOrDefault(s =>
                    {
                        string storedUsername = EncDec.Decrypt((string)s.Element("Username"));
                        string storedPassword = EncDec.Decrypt((string)s.Element("Password"));
                        return storedUsername == username && storedPassword == password;
                    });

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
                var encryptedUsername = EncDec.Encrypt(username);
                var encryptedPassword = EncDec.Encrypt(password);

                var member = doc.Descendants("Members")
                    .FirstOrDefault(m =>
                    {
                        string storedUsername = EncDec.Decrypt((string)m.Element("Username"));
                        string storedPassword = EncDec.Decrypt((string)m.Element("Password"));
                        return storedUsername == username && storedPassword == password;
                    });

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