using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using EncryptionDecryption;

namespace Application
{
    // Developed by Chris Harris and Ilia Sorokin
    public partial class Admin : Page
    {
        private const string CaptchaSessionKey = "CaptchaCode"; // Constant to store CAPTCHA session key

        // Page_Load method, triggers when the page is loaded
        protected void Page_Load(object sender, EventArgs e)
        {
            // Check if staff user is not logged in, redirect to login page
            if (Session["StaffUser"] == null)
            {
                Response.Redirect("Login.aspx");
            }

            // Bind the delete confirmation event to the DeleteStaffMember method
            DeleteConfirmationControl.DeleteConfirmed += DeleteStaffMember;

            // Generate CAPTCHA on the first page load
            if (!IsPostBack)
            {
                GenerateCaptcha();
            }
        }

        // Event handler for the Add Staff button click
        protected void btnAddStaff_Click(object sender, EventArgs e)
        {
            string username = txtAddUsername.Text.Trim();
            string password = txtAddPassword.Text.Trim();
            string enteredCaptcha = txtCaptchaInput.Text.Trim();
            string expectedCaptcha = Session[CaptchaSessionKey]?.ToString();

            // Check if any field is empty
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(enteredCaptcha))
            {
                lblMessage.Text = "Please fill in all fields."; // Show message for missing fields
                return;
            }

            // Validate CAPTCHA input
            if (enteredCaptcha != expectedCaptcha)
            {
                lblMessage.Text = "Incorrect CAPTCHA. Please try again."; // Show message for incorrect CAPTCHA
                GenerateCaptcha();
                return;
            }

            try
            {
                // Load staff XML document
                var staffDoc = XDocument.Load(Server.MapPath("~/Staff.xml"));
                string encryptedUsername = EncDec.Encrypt(username);

                // Check if the username already exists
                if (staffDoc.Descendants("Staff").Any(s =>
                    EncDec.Decrypt((string)s.Element("Username")) == username))
                {
                    lblMessage.Text = "This username already exists in Staff records."; // Show message if username exists
                    return;
                }

                // Add the new staff member with encrypted credentials
                staffDoc.Root.Add(new XElement("Staff",
                    new XElement("Username", EncDec.Encrypt(username)),
                    new XElement("Password", EncDec.Encrypt(password))));
                staffDoc.Save(Server.MapPath("~/Staff.xml"));

                // Clear the form and show success message
                lblMessage.Text = "Staff member added successfully!";
                activityLog.LogAddStaff(username);
                txtAddUsername.Text = "";
                txtAddPassword.Text = "";
                txtCaptchaInput.Text = "";
                GenerateCaptcha();
            }
            catch (Exception ex)
            {
                // Show error message if something goes wrong
                lblMessage.Text = $"Error: {ex.Message}";
            }
        }

        // Delete staff member by username
        private void DeleteStaffMember(object sender, string usernameToDelete)
        {
            try
            {
                var staffDoc = XDocument.Load(Server.MapPath("~/Staff.xml"));

                var staffToDelete = staffDoc.Descendants("Staff")
                    .FirstOrDefault(s => EncDec.Decrypt((string)s.Element("Username")) == usernameToDelete);

                // Check if the staff member exists and delete
                if (staffToDelete != null)
                {
                    staffToDelete.Remove();
                    staffDoc.Save(Server.MapPath("~/Staff.xml"));
                    lblMessage.Text = $"Staff member '{usernameToDelete}' was deleted successfully.";
                    activityLog.LogStaffDeletion(usernameToDelete); // Log the deletion
                }
                else
                {
                    lblMessage.Text = $"Staff member '{usernameToDelete}' was not found."; // Show message if not found
                }
            }
            catch (Exception ex)
            {
                // Show error message if deletion fails
                lblMessage.Text = $"Error while deleting the staff member: {ex.Message}";
            }
        }

        // Event handler to view all staff members
        protected void btnViewAllStaff_Click(object sender, EventArgs e)
        {
            try
            {
                var staffDoc = XDocument.Load(Server.MapPath("~/Staff.xml"));
                var staffList = staffDoc.Descendants("Staff")
                    .Select(s => EncDec.Decrypt((string)s.Element("Username")))
                    .ToList();

                // Display list of staff members or message if no staff
                lblStaffList.Text = staffList.Any()
                    ? "<strong>Staff Members:</strong><br />" + string.Join("<br />", staffList)
                    : "No staff members found.";
            }
            catch (Exception ex)
            {
                // Show error message if loading staff fails
                lblStaffList.Text = $"Error: {ex.Message}";
            }
        }

        // CAPTCHA refresh event handler
        protected void btnRefreshCaptcha_Click(object sender, EventArgs e)
        {
            GenerateCaptcha(); // Generate a new CAPTCHA code
        }

        // Method to generate a random CAPTCHA code and display it as an image
        private void GenerateCaptcha()
        {
            string captchaCode = GenerateRandomCaptchaCode(); // Generate CAPTCHA code
            Session[CaptchaSessionKey] = captchaCode; // Store CAPTCHA in session

            using (Bitmap bmp = new Bitmap(150, 50))
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.White); // Set background color
                using (Font font = new Font("Arial", 20, FontStyle.Bold))
                {
                    g.DrawString(captchaCode, font, Brushes.Black, 10, 10); // Draw CAPTCHA code on image
                }

                using (MemoryStream ms = new MemoryStream())
                {
                    bmp.Save(ms, ImageFormat.Png); // Save image as PNG
                    byte[] byteImage = ms.ToArray();
                    string base64String = Convert.ToBase64String(byteImage);
                    imgCaptcha.ImageUrl = $"data:image/png;base64,{base64String}"; // Display CAPTCHA image
                }
            }
        }

        // Helper method to generate a random 6-character CAPTCHA code
        private string GenerateRandomCaptchaCode()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            Random random = new Random();
            return new string(Enumerable.Repeat(chars, 6).Select(s => s[random.Next(s.Length)]).ToArray());
        }

        // Log out event handler
        protected void btnLogOut_Click(object sender, EventArgs e)
        {
            activityLog.LogLogout();
            Session.Clear(); // Clear session data
            Response.Redirect("Default.aspx"); // Redirect to default page
        }

        // Redirect to home page event handler
        protected void btnGoToDefault_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx"); // Redirect to home page
        }
    }
}
