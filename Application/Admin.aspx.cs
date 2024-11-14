using System;
using System.Linq;
using System.Web.UI;
using System.Xml.Linq;

namespace Application
{
    public partial class Admin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Check if the user is logged in as a staff member
            if (Session["StaffUser"] == null)
            {
                // Redirect to the login page if not logged in
                Response.Redirect("Login.aspx");
            }
        }

        protected void btnAddStaff_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                lblMessage.Text = "Please enter both a username and password.";
                return;
            }

            try
            {
                // Load the XML file
                var doc = XDocument.Load(Server.MapPath("~/Staff.xml"));

                // Check if the username already exists
                bool userExists = doc.Descendants("Staff")
                    .Any(s => (string)s.Element("Username") == username);

                if (userExists)
                {
                    lblMessage.Text = "This username already exists.";
                    return;
                }

                // Create a new staff entry
                XElement newStaff = new XElement("Staff",
                    new XElement("Username", username),
                    new XElement("Password", password) // Consider hashing here for security
                );

                // Add the new entry and save the XML file
                doc.Root.Add(newStaff);
                doc.Save(Server.MapPath("~/Staff.xml"));

                lblMessage.Text = "Staff member added successfully!";
                txtUsername.Text = "";
                txtPassword.Text = "";
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error: " + ex.Message;
            }
        }

        protected void btnShowDelete_Click(object sender, EventArgs e)
        {
            // Show the panel with the delete text box and confirm button
            pnlDeleteStaff.Visible = true;
        }

        protected void btnDeleteStaff_Click(object sender, EventArgs e)
        {
            string usernameToDelete = txtDeleteUsername.Text;

            if (string.IsNullOrWhiteSpace(usernameToDelete))
            {
                lblMessage.Text = "Please enter a username to delete.";
                return;
            }

            if (usernameToDelete == "TA")
            {
                lblMessage.Text = "You cannot delete the TA account.";
                return;
            }

            try
            {
                // Load the XML file
                var doc = XDocument.Load(Server.MapPath("~/Staff.xml"));

                // Find the staff member element to delete
                var staffToDelete = doc.Descendants("Staff")
                                       .FirstOrDefault(s => (string)s.Element("Username") == usernameToDelete);

                if (staffToDelete != null)
                {
                    // Remove the staff member and save the XML file
                    staffToDelete.Remove();
                    doc.Save(Server.MapPath("~/Staff.xml"));

                    lblMessage.Text = $"Staff member '{usernameToDelete}' deleted successfully.";
                    txtDeleteUsername.Text = "";
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
            // Redirect back to the Login page
            Response.Redirect("Login.aspx");
        }

        protected void btnViewAllStaff_Click(object sender, EventArgs e)
        {
            try
            {
                var doc = XDocument.Load(Server.MapPath("~/Staff.xml"));

                // Retrieve all staff usernames
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