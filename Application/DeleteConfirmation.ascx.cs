using System;
using System.Web.UI;

namespace Application.Controls
{
    public partial class DeleteConfirmation : UserControl
    {
        public event EventHandler<string> DeleteConfirmed;

        protected void btnConfirmDelete_Click(object sender, EventArgs e)
        {
            string usernameToDelete = txtDeleteUsername.Text.Trim();

            if (!string.IsNullOrWhiteSpace(usernameToDelete) && usernameToDelete != "TA")
            {
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