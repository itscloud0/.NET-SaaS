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
                // Check if username or password exists in Member.xml
                var memberDoc = XDocument.Load(Server.MapPath("~/Member.xml"));
                bool memberExists = memberDoc.Descendants("Members")
                    .Any(m => (string)m.Element("Username") == username || (string)m.Element("Password") == password);

                if (memberExists)
                {
                    lblMessage.Text = "The username or password already exists in Member records.";
                    return;
                }

                // Load Staff.xml and check if username already exists
                var staffDoc = XDocument.Load(Server.MapPath("~/Staff.xml"));
                bool userExists = staffDoc.Descendants("Staff")
                    .Any(s => (string)s.Element("Username") == username);

                if (userExists)
                {
                    lblMessage.Text = "This username already exists in Staff records.";
                    return;
                }

                // Add the new staff member
                XElement newStaff = new XElement("Staff",
                    new XElement("Username", username),
                    new XElement("Password", password) // Consider hashing for security
                );

                staffDoc.Root.Add(newStaff);
                staffDoc.Save(Server.MapPath("~/Staff.xml"));

                lblMessage.Text = "Staff member added successfully!";
                txtUsername.Text = "";
                txtPassword.Text = "";
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error: " + ex.Message;
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
                var doc = XDocument.Load(Server.MapPath("~/Staff.xml"));

                var staffToDelete = doc.Descendants("Staff")
                                       .FirstOrDefault(s => (string)s.Element("Username") == usernameToDelete);

                if (staffToDelete != null)
                {
                    staffToDelete.Remove();
                    doc.Save(Server.MapPath("~/Staff.xml"));

                    lblMessage.Text = $"Staff member '{usernameToDelete}' deleted successfully.";
                }
                else
                {
                    lblMessage.Text = "Staff member not found.";
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error deleting staff member: " + ex.Message;
            }
        }

        protected void btnBackToLogin_Click(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
        }

        protected void btnViewAllStaff_Click(object sender, EventArgs e)
        {
            try
            {
                var doc = XDocument.Load(Server.MapPath("~/Staff.xml"));

                var staffList = doc.Descendants("Staff")
                                   .Select(s => (string)s.Element("Username"))
                                   .ToList();

                if (staffList.Any())
                {
                    lblStaffList.Text = "<strong>Staff Members:</strong><br />" +
                        string.Join("<br />", staffList);
                }
                else
                {
                    lblStaffList.Text = "No staff members found.";
                }
            }
            catch (Exception ex)
            {
                lblStaffList.Text = "Error loading staff list: " + ex.Message;
            }
        }
    }
}