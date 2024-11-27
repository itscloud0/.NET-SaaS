using System;
using System.Linq;
using System.Web.UI;
using System.Xml.Linq;
using EncryptionDecryption;

namespace Application
{
    public partial class MemberPage : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["MemberUser"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            else
            {
                lblMemberWelcome.Text = $"Welcome, {Session["MemberUser"]}!";
            }
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("Login.aspx");
        }

        protected void btnChangePassword_Click(object sender, EventArgs e)
        {
            string newPassword = txtNewPassword.Text.Trim();

            if (string.IsNullOrWhiteSpace(newPassword))
            {
                lblMessage.Text = "Please enter a new password.";
                return;
            }

            try
            {
                string filePath = Server.MapPath("~/Member.xml");
                var doc = XDocument.Load(filePath);

                string username = Session["MemberUser"].ToString();
                var member = doc.Descendants("Members")
                    .FirstOrDefault(m =>
                        EncDec.Decrypt((string)m.Element("Username")) == username);

                if (member != null)
                {
                    member.Element("Password").Value = EncDec.Encrypt(newPassword);
                    doc.Save(filePath);

                    lblMessage.CssClass = "success-message";
                    lblMessage.Text = "Password changed successfully!";
                }
                else
                {
                    lblMessage.CssClass = "error-message";
                    lblMessage.Text = "Error: Member not found.";
                }
            }
            catch (Exception ex)
            {
                lblMessage.CssClass = "error-message";
                lblMessage.Text = $"An error occurred: {ex.Message}";
            }
        }

        protected void btnDeleteAccount_Click(object sender, EventArgs e)
        {
            try
            {
                string filePath = Server.MapPath("~/Member.xml");
                var doc = XDocument.Load(filePath);

                string username = Session["MemberUser"].ToString();
                var member = doc.Descendants("Members")
                    .FirstOrDefault(m =>
                        EncDec.Decrypt((string)m.Element("Username")) == username);

                if (member != null)
                {
                    member.Remove();
                    doc.Save(filePath);

                    lblMessage.CssClass = "success-message";
                    lblMessage.Text = "Account deleted successfully!";
                    Session.Clear(); // Log out user
                    Response.Redirect("Login.aspx");
                }
                else
                {
                    lblMessage.CssClass = "error-message";
                    lblMessage.Text = "Error: Member not found.";
                }
            }
            catch (Exception ex)
            {
                lblMessage.CssClass = "error-message";
                lblMessage.Text = $"An error occurred: {ex.Message}";
            }
        }
    }
}