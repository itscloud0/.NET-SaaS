// Developed by Ilia Sorokin
// This ASP.NET Web Forms application provides encryption and decryption functionality using the EncDec library.

using System;
using EncryptionDecryption;

namespace Application
{
    public partial class EncryptValues : System.Web.UI.Page
    {
        // This event handler is triggered when the page is loaded.
        protected void Page_Load(object sender, EventArgs e)
        {
            // Only clear the message label if the page is loaded for the first time (not a postback).
            if (!IsPostBack)
            {
                lblMessage.Text = "";
            }
        }

        // This event handler is triggered when the "Encrypt" button is clicked.
        protected void btnEncrypt_Click(object sender, EventArgs e)
        {
            try
            {
                // Retrieve and trim the user input from the text box.
                string input = txtInput.Text.Trim();

                // Validate that the input is not empty.
                if (string.IsNullOrEmpty(input))
                {
                    lblMessage.Text = "Please enter text to encrypt.";
                    pnlResults.Visible = false; // Hide the results panel if validation fails.
                    return;
                }

                // Encrypt the input using the EncDec library.
                string encrypted = EncDec.Encrypt(input);

                // Display the original input and the encrypted result.
                lblInputText.Text = input;
                lblResult.Text = encrypted;
                pnlResults.Visible = true; // Show the results panel.
                lblMessage.Text = ""; // Clear any previous messages.
            }
            catch (Exception ex)
            {
                // Handle any errors during the encryption process and display an error message.
                lblMessage.Text = "Error during encryption: " + ex.Message;
                pnlResults.Visible = false; // Hide the results panel if an error occurs.
            }
        }

        // This event handler is triggered when the "Decrypt" button is clicked.
        protected void btnDecrypt_Click(object sender, EventArgs e)
        {
            try
            {
                // Retrieve and trim the user input from the text box.
                string input = txtInput.Text.Trim();

                // Validate that the input is not empty.
                if (string.IsNullOrEmpty(input))
                {
                    lblMessage.Text = "Please enter text to decrypt.";
                    pnlResults.Visible = false; // Hide the results panel if validation fails.
                    return;
                }

                // Decrypt the input using the EncDec library.
                string decrypted = EncDec.Decrypt(input);

                // Display the original input and the decrypted result.
                lblInputText.Text = input;
                lblResult.Text = decrypted;
                pnlResults.Visible = true; // Show the results panel.
                lblMessage.Text = ""; // Clear any previous messages.
            }
            catch (Exception ex)
            {
                // Handle any errors during the decryption process and display an error message.
                lblMessage.Text = "Error during decryption. Make sure the input is a valid encrypted string.";
                pnlResults.Visible = false; // Hide the results panel if an error occurs.
            }
        }
    }
}
