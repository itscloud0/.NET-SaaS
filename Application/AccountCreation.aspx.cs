using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Xml.Linq;
using EncryptionDecryption;

// Developed by Chris Harris
namespace Application
{
    public partial class AccountCreation : Page
    {
        // Key used for storing CAPTCHA code in the session
        private const string CaptchaSessionKey = "CaptchaCode";

        // Page_Load event is triggered when the page is loaded
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) // Only generate CAPTCHA on the first load, not on postbacks
            {
                GenerateCaptcha();
            }
        }

        // Event triggered when the 'Create Account' button is clicked
        protected void btnCreateAccount_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim(); // Retrieve and trim the username
            string password = txtPassword.Text.Trim(); // Retrieve and trim the password
            string enteredCaptcha = txtCaptchaInput.Text.Trim(); // Retrieve the entered CAPTCHA
            string expectedCaptcha = Session[CaptchaSessionKey]?.ToString(); // Retrieve the expected CAPTCHA from the session

            // Validate that all required fields are filled
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(enteredCaptcha))
            {
                lblMessage.Text = "Please fill in all fields."; // Display an error message
                lblMessage.CssClass = "error-message"; // Set the CSS class for error styling
                return;
            }

            // Validate that the entered CAPTCHA matches the expected CAPTCHA
            if (enteredCaptcha != expectedCaptcha)
            {
                lblMessage.Text = "Incorrect CAPTCHA. Please try again."; // Display an error message
                lblMessage.CssClass = "error-message"; // Set the CSS class for error styling
                GenerateCaptcha(); // Regenerate a new CAPTCHA
                return;
            }

            try
            {
                string filePath = Server.MapPath("~/Member.xml"); // Get the file path for Member.xml

                // Load Member.xml if it exists; otherwise, create a new XML document
                XDocument doc = File.Exists(filePath) ? XDocument.Load(filePath) : new XDocument(new XElement("RegMembers"));

                // Check if the username already exists in the XML file
                if (doc.Descendants("Members")
                    .Any(m => EncDec.Decrypt((string)m.Element("Username")) == username))
                {
                    lblMessage.Text = "This username already exists. Please choose a different username."; // Display an error message
                    lblMessage.CssClass = "error-message"; // Set the CSS class for error styling
                    return;
                }

                // Create a new XML element with encrypted username and password
                XElement newMember = new XElement("Members",
                    new XElement("Username", EncDec.Encrypt(username)),
                    new XElement("Password", EncDec.Encrypt(password))
                );

                doc.Root.Add(newMember); // Add the new member to the XML document
                doc.Save(filePath); // Save the updated XML document

                lblMessage.Text = "Account created successfully!"; // Display a success message
                lblMessage.CssClass = "success-message"; // Set the CSS class for success styling
                activityLog.LogAccCreation(username); //Add to log
                // Clear the input fields and regenerate CAPTCHA
                txtUsername.Text = "";
                txtPassword.Text = "";
                txtCaptchaInput.Text = "";
                GenerateCaptcha();
            }
            catch (Exception ex)
            {
                lblMessage.Text = "An error occurred: " + ex.Message; // Display an error message if an exception occurs
                lblMessage.CssClass = "error-message"; // Set the CSS class for error styling
            }
        }

        // Event triggered when the 'Back' button is clicked
        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx"); // Redirect to the login page
        }

        // Event triggered when the 'Refresh CAPTCHA' button is clicked
        protected void btnRefreshCaptcha_Click(object sender, EventArgs e)
        {
            GenerateCaptcha(); // Generate a new CAPTCHA
        }

        // Method to generate and display a CAPTCHA image
        private void GenerateCaptcha()
        {
            string captchaCode = GenerateRandomCaptchaCode(); // Generate a random CAPTCHA code
            Session[CaptchaSessionKey] = captchaCode; // Store the CAPTCHA code in the session

            // Create a bitmap to draw the CAPTCHA image
            using (Bitmap bmp = new Bitmap(150, 50))
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.White); // Set the background color to white
                using (Font font = new Font("Arial", 20, FontStyle.Bold)) // Define the font for the CAPTCHA text
                {
                    g.DrawString(captchaCode, font, Brushes.Black, 10, 10); // Draw the CAPTCHA text on the image
                }

                using (MemoryStream ms = new MemoryStream()) // Save the image to a memory stream
                {
                    bmp.Save(ms, ImageFormat.Png); // Save the image in PNG format
                    byte[] byteImage = ms.ToArray(); // Convert the image to a byte array
                    string base64String = Convert.ToBase64String(byteImage); // Convert the byte array to a Base64 string
                    imgCaptcha.ImageUrl = $"data:image/png;base64,{base64String}"; // Set the image source to the Base64 string
                }
            }
        }

        // Method to generate a random 6-character CAPTCHA code
        private string GenerateRandomCaptchaCode()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789"; // Character set for the CAPTCHA
            Random random = new Random(); // Create a random number generator
            return new string(Enumerable.Repeat(chars, 6).Select(s => s[random.Next(s.Length)]).ToArray()); // Generate the random code
        }
    }
}
