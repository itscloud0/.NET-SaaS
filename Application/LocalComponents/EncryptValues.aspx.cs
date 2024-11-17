using System;
using EncryptionDecryption;

namespace Application
{
    public partial class EncryptValues : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblMessage.Text = "";
            }
        }

        protected void btnEncrypt_Click(object sender, EventArgs e)
        {
            try
            {
                string input = txtInput.Text.Trim();
                if (string.IsNullOrEmpty(input))
                {
                    lblMessage.Text = "Please enter text to encrypt.";
                    pnlResults.Visible = false;
                    return;
                }

                string encrypted = EncDec.Encrypt(input);

                lblInputText.Text = input;
                lblResult.Text = encrypted;
                pnlResults.Visible = true;
                lblMessage.Text = "";
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error during encryption: " + ex.Message;
                pnlResults.Visible = false;
            }
        }

        protected void btnDecrypt_Click(object sender, EventArgs e)
        {
            try
            {
                string input = txtInput.Text.Trim();
                if (string.IsNullOrEmpty(input))
                {
                    lblMessage.Text = "Please enter text to decrypt.";
                    pnlResults.Visible = false;
                    return;
                }

                string decrypted = EncDec.Decrypt(input);

                lblInputText.Text = input;
                lblResult.Text = decrypted;
                pnlResults.Visible = true;
                lblMessage.Text = "";
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error during decryption. Make sure the input is a valid encrypted string.";
                pnlResults.Visible = false;
            }
        }
    }
}