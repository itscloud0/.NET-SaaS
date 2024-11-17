using System;
using System.Web.UI;

namespace Application.Controls
{
    // Developed by Chris Harris
    public partial class DeleteConfirmation : UserControl
    {
        // Event that is triggered when a deletion is confirmed, passing the username to delete
        public event EventHandler<string> DeleteConfirmed;

        // Handler for the Confirm Delete button click
        protected void btnConfirmDelete_Click(object sender, EventArgs e)
        {
            // Retrieve the username to delete from the text box
            string usernameToDelete = txtDeleteUsername.Text.Trim();

            // Check if the username is valid and not the TA account
            if (!string.IsNullOrWhiteSpace(usernameToDelete) && usernameToDelete != "TA")
            {
                // Trigger the DeleteConfirmed event, passing the username to delete
                DeleteConfirmed?.Invoke(this, usernameToDelete);
            }
            else
            {
                // Display appropriate message if username is invalid or if it's the TA account
                lblMessage.Text = usernameToDelete == "TA"
                    ? "You cannot delete the TA account."
                    : "Please enter a valid username.";
            }
        }
    }
}
