using System;
using System.Linq;
using System.Web.UI;
using System.Xml.Linq;

namespace Application
{
    public partial class Admin : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Check if the user is logged in as a staff member
            if (Session["StaffUser"] == null)
            {
                Response.Redirect("Login.aspx");
            }

            // Subscribe to the DeleteConfirmed event of the DeleteConfirmationControl
            DeleteConfirmationControl.DeleteConfirmed += DeleteStaffMember;
        }

        protected void btnAddStaff_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                lblMessage.Text = "Please enter both a username and password.";
                return;
            }

            try
            {
                var staffDoc = XDocument.Load(Server.MapPath("~/Staff.xml"));
                if (staffDoc.Descendants("Staff").Any(s => (string)s.Element("Username") == username))
                {
                    lblMessage.Text = "This username already exists in Staff records.";
                    return;
                }

                staffDoc.Root.Add(new XElement("Staff",
                    new XElement("Username", username),
                    new XElement("Password", password)));
                staffDoc.Save(Server.MapPath("~/Staff.xml"));

                lblMessage.Text = "Staff member added successfully!";
                txtUsername.Text = "";
                txtPassword.Text = "";
            }
            catch (Exception ex)
            {
                lblMessage.Text = $"Error: {ex.Message}";
            }
        }

        private void DeleteStaffMember(object sender, string usernameToDelete)
        {
            if (usernameToDelete == "TA")
            {
                lblMessage.Text = "You cannot delete the TA account.";
                return;
            }

            try
            {
                var staffDoc = XDocument.Load(Server.MapPath("~/Staff.xml"));
                var staffToDelete = staffDoc.Descendants("Staff")
                    .FirstOrDefault(s => (string)s.Element("Username") == usernameToDelete);

                if (staffToDelete != null)
                {
                    staffToDelete.Remove();
                    staffDoc.Save(Server.MapPath("~/Staff.xml"));
                    lblMessage.Text = $"Staff member '{usernameToDelete}' deleted successfully.";
                }
                else
                {
                    lblMessage.Text = "Staff member not found.";
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = $"Error: {ex.Message}";
            }
        }

        protected void btnLogOut_Click(object sender, EventArgs e)
        {
            Session.Clear(); // Clear session data
            Response.Redirect("Default.aspx"); // Redirect to Default.aspx
        }

        protected void btnGoToDefault_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }

        protected void btnViewAllStaff_Click(object sender, EventArgs e)
        {
            try
            {
                var staffDoc = XDocument.Load(Server.MapPath("~/Staff.xml"));
                var staffList = staffDoc.Descendants("Staff")
                    .Select(s => (string)s.Element("Username")).ToList();

                lblStaffList.Text = staffList.Any()
                    ? "<strong>Staff Members:</strong><br />" + string.Join("<br />", staffList)
                    : "No staff members found.";
            }
            catch (Exception ex)
            {
                lblStaffList.Text = $"Error: {ex.Message}";
            }
        }
    }
}