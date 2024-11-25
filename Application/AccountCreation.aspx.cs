using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Xml.Linq;
using EncryptionDecryption;

namespace Application
{
    public partial class AccountCreation : Page
    {
        private const string CaptchaSessionKey = "CaptchaCode";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GenerateCaptcha();
            }
        }

        protected void btnCreateAccount_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();
            string enteredCaptcha = txtCaptchaInput.Text.Trim();
            string expectedCaptcha = Session[CaptchaSessionKey]?.ToString();

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(enteredCaptcha))
            {
                lblMessage.Text = "Please fill in all fields.";
                lblMessage.CssClass = "error-message";
                return;
            }

            if (enteredCaptcha != expectedCaptcha)
            {
                lblMessage.Text = "Incorrect CAPTCHA. Please try again.";
                lblMessage.CssClass = "error-message";
                GenerateCaptcha();
                return;
            }

            try
            {
                string filePath = Server.MapPath("~/Member.xml");

                // Load or create Member.xml
                XDocument doc = File.Exists(filePath) ? XDocument.Load(filePath) : new XDocument(new XElement("StaffMembers"));

                // Check for existing username
                if (doc.Descendants("Members")
                    .Any(m => EncDec.Decrypt((string)m.Element("Username")) == username))
                {
                    lblMessage.Text = "This username already exists. Please choose a different username.";
                    lblMessage.CssClass = "error-message";
                    return;
                }

                // Encrypt and save the username and password
                XElement newMember = new XElement("Members",
                    new XElement("Username", EncDec.Encrypt(username)),
                    new XElement("Password", EncDec.Encrypt(password))
                );

                doc.Root.Add(newMember);
                doc.Save(filePath);

                lblMessage.Text = "Account created successfully!";
                lblMessage.CssClass = "success-message";

                // Clear inputs
                txtUsername.Text = "";
                txtPassword.Text = "";
                txtCaptchaInput.Text = "";
                GenerateCaptcha();
            }
            catch (Exception ex)
            {
                lblMessage.Text = "An error occurred: " + ex.Message;
                lblMessage.CssClass = "error-message";
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
        }

        protected void btnRefreshCaptcha_Click(object sender, EventArgs e)
        {
            GenerateCaptcha();
        }

        private void GenerateCaptcha()
        {
            string captchaCode = GenerateRandomCaptchaCode();
            Session[CaptchaSessionKey] = captchaCode;

            using (Bitmap bmp = new Bitmap(150, 50))
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.White);
                using (Font font = new Font("Arial", 20, FontStyle.Bold))
                {
                    g.DrawString(captchaCode, font, Brushes.Black, 10, 10);
                }

                using (MemoryStream ms = new MemoryStream())
                {
                    bmp.Save(ms, ImageFormat.Png);
                    byte[] byteImage = ms.ToArray();
                    string base64String = Convert.ToBase64String(byteImage);
                    imgCaptcha.ImageUrl = $"data:image/png;base64,{base64String}";
                }
            }
        }

        private string GenerateRandomCaptchaCode()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            Random random = new Random();
            return new string(Enumerable.Repeat(chars, 6).Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}