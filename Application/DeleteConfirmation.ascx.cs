using System;
using System.Linq;
using System.Web.UI;

namespace Application.Controls
{
    public partial class DeleteConfirmation : UserControl
    {
        // Event for parent page to handle deletion
        public event EventHandler<string> DeleteConfirmed;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GenerateCaptcha();
            }
        }

        private void GenerateCaptcha()
        {
            // Define the set of characters to use in the CAPTCHA
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

            // Generate a random CAPTCHA code with 6 characters
            Random random = new Random();
            string captchaCode = new string(Enumerable.Repeat(chars, 6)
                                                      .Select(s => s[random.Next(s.Length)]).ToArray());

            lblCaptcha.Text = captchaCode;

            // Store the CAPTCHA in ViewState to validate later
            ViewState["CaptchaCode"] = captchaCode;
        }

        protected void btnConfirmDelete_Click(object sender, EventArgs e)
        {
            string usernameToDelete = txtDeleteUsername.Text.Trim();
            string enteredCaptcha = txtCaptchaInput.Text.Trim();
            string expectedCaptcha = ViewState["CaptchaCode"] as string;

            // Validate CAPTCHA
            if (enteredCaptcha != expectedCaptcha)
            {
                lblMessage.Text = "Incorrect CAPTCHA. Please try again.";
                GenerateCaptcha(); // Generate a new CAPTCHA for retry
                return;
            }

            if (!string.IsNullOrWhiteSpace(usernameToDelete) && usernameToDelete != "TA")
            {
                // Raise the DeleteConfirmed event and pass the username if CAPTCHA is correct
                DeleteConfirmed?.Invoke(this, usernameToDelete);
            }
            else
            {
                lblMessage.Text = usernameToDelete == "TA"
                    ? "You cannot delete the TA account."
                    : "Please enter a valid username.";
            }
        }
    }
}