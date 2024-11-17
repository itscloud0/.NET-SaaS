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
    public partial class Admin : Page
    {
        private const string CaptchaSessionKey = "CaptchaCode";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["StaffUser"] == null)
            {
                Response.Redirect("Login.aspx");
            }

            DeleteConfirmationControl.DeleteConfirmed += DeleteStaffMember;

            if (!IsPostBack)
            {
                GenerateCaptcha();
            }
        }

        protected void btnAddStaff_Click(object sender, EventArgs e)
        {
            string username = txtAddUsername.Text.Trim();
            string password = txtAddPassword.Text.Trim();
            string enteredCaptcha = txtCaptchaInput.Text.Trim();
            string expectedCaptcha = Session[CaptchaSessionKey]?.ToString();

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(enteredCaptcha))
            {
                lblMessage.Text = "Please fill in all fields.";
                return;
            }

            if (enteredCaptcha != expectedCaptcha)
            {
                lblMessage.Text = "Incorrect CAPTCHA. Please try again.";
                GenerateCaptcha();
                return;
            }

            try
            {
                var staffDoc = XDocument.Load(Server.MapPath("~/Staff.xml"));
                string encryptedUsername = EncDec.Encrypt(username);

                if (staffDoc.Descendants("Staff").Any(s =>
                    EncDec.Decrypt((string)s.Element("Username")) == username))
                {
                    lblMessage.Text = "This username already exists in Staff records.";
                    return;
                }

                // Store encrypted values
                staffDoc.Root.Add(new XElement("Staff",
                    new XElement("Username", EncDec.Encrypt(username)),
                    new XElement("Password", EncDec.Encrypt(password))));
                staffDoc.Save(Server.MapPath("~/Staff.xml"));

                lblMessage.Text = "Staff member added successfully!";
                txtAddUsername.Text = "";
                txtAddPassword.Text = "";
                txtCaptchaInput.Text = "";
                GenerateCaptcha();
            }
            catch (Exception ex)
            {
                lblMessage.Text = $"Error: {ex.Message}";
            }
        }

        private void DeleteStaffMember(object sender, string usernameToDelete)
        {
            try
            {
                var staffDoc = XDocument.Load(Server.MapPath("~/Staff.xml"));

                var staffToDelete = staffDoc.Descendants("Staff")
                    .FirstOrDefault(s => EncDec.Decrypt((string)s.Element("Username")) == usernameToDelete);

                if (staffToDelete != null)
                {
                    staffToDelete.Remove();
                    staffDoc.Save(Server.MapPath("~/Staff.xml"));
                    lblMessage.Text = $"Staff member '{usernameToDelete}' was deleted successfully.";
                }
                else
                {
                    lblMessage.Text = $"Staff member '{usernameToDelete}' was not found.";
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = $"Error while deleting the staff member: {ex.Message}";
            }
        }

        protected void btnViewAllStaff_Click(object sender, EventArgs e)
        {
            try
            {
                var staffDoc = XDocument.Load(Server.MapPath("~/Staff.xml"));
                var staffList = staffDoc.Descendants("Staff")
                    .Select(s => EncDec.Decrypt((string)s.Element("Username")))
                    .ToList();

                lblStaffList.Text = staffList.Any()
                    ? "<strong>Staff Members:</strong><br />" + string.Join("<br />", staffList)
                    : "No staff members found.";
            }
            catch (Exception ex)
            {
                lblStaffList.Text = $"Error: {ex.Message}";
            }
        }

        // Keeping existing CAPTCHA-related methods unchanged
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

        protected void btnLogOut_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("Default.aspx");
        }

        protected void btnGoToDefault_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }
    }
}