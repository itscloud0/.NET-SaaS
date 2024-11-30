using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using EncryptionDecryption;

//Developed by Cole Eastman
namespace Application
{
    public partial class MemberPage : Page
    {
        // Page_Load is triggered when the page is loaded.
        // Ensures that the user is logged in before accessing this page.
        protected void Page_Load(object sender, EventArgs e)
        {
            // If the session does not contain a member user, redirect to the login page.
            if (Session["MemberUser"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            else
            {
                // Display a personalized welcome message for the logged-in user.
                lblMemberWelcome.Text = $"Welcome, {Session["MemberUser"]}!";
            }
        }

        // Handles the logout functionality.
        protected void btnLogout_Click(object sender, EventArgs e)
        {
            activityLog.LogLogout();
            // Clear the session to log out the user.
            Session.Clear();
            // Redirect the user to the login page.
            Response.Redirect("Login.aspx");
        }

        // Handles the functionality for changing the user's password.
        protected void btnChangePassword_Click(object sender, EventArgs e)
        {
            // Retrieve the new password from the input field.
            string newPassword = txtNewPassword.Text.Trim();

            // Validate that the new password is not empty or whitespace.
            if (string.IsNullOrWhiteSpace(newPassword))
            {
                lblMessage.Text = "Please enter a new password.";
                return;
            }

            try
            {
                // Load the XML file containing member details.
                string filePath = Server.MapPath("~/Member.xml");
                var doc = XDocument.Load(filePath);

                // Retrieve the currently logged-in user's username.
                string username = Session["MemberUser"].ToString();

                // Find the corresponding member entry in the XML file by decrypting the username.
                var member = doc.Descendants("Members")
                    .FirstOrDefault(m =>
                        EncDec.Decrypt((string)m.Element("Username")) == username);

                // If the member is found, update the password.
                if (member != null)
                {
                    // Encrypt the new password and save it to the XML file.
                    member.Element("Password").Value = EncDec.Encrypt(newPassword);
                    doc.Save(filePath);

                    // Display a success message to the user.
                    lblMessage.CssClass = "success-message";
                    lblMessage.Text = "Password changed successfully!";
                    activityLog.LogChangePassword(); // Log the password change
                }
                else
                {
                    // Display an error message if the member is not found.
                    lblMessage.CssClass = "error-message";
                    lblMessage.Text = "Error: Member not found.";
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions and display an error message.
                lblMessage.CssClass = "error-message";
                lblMessage.Text = $"An error occurred: {ex.Message}";
            }
        }

        // Handles the functionality for deleting the user's account.
        protected void btnDeleteAccount_Click(object sender, EventArgs e)
        {
            try
            {
                // Load the XML file containing member details.
                string filePath = Server.MapPath("~/Member.xml");
                var doc = XDocument.Load(filePath);

                // Retrieve the currently logged-in user's username.
                string username = Session["MemberUser"].ToString();

                // Find the corresponding member entry in the XML file by decrypting the username.
                var member = doc.Descendants("Members")
                    .FirstOrDefault(m =>
                        EncDec.Decrypt((string)m.Element("Username")) == username);

                // If the member is found, delete the entry.
                if (member != null)
                {
                    // Remove the member entry and save the changes to the XML file.
                    member.Remove();
                    doc.Save(filePath);

                    // Display a success message and log out the user.
                    lblMessage.CssClass = "success-message";
                    lblMessage.Text = "Account deleted successfully!";
                    activityLog.LogAccDeletion(); //Log the deletion
                    Session.Clear(); // Clear the session
                    Response.Redirect("Login.aspx"); // Redirect to login page
                }
                else
                {
                    // Display an error message if the member is not found.
                    lblMessage.CssClass = "error-message";
                    lblMessage.Text = "Error: Member not found.";
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions and display an error message.
                lblMessage.CssClass = "error-message";
                lblMessage.Text = $"An error occurred: {ex.Message}";
            }
        }
    }
}
