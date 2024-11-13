using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Application
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Check if the user is logged in
            if (Session["LoggedInUser"] != null)
            {
                lblStatus.Text = "Logged in as: " + Session["LoggedInUser"];
            }
            else
            {
                lblStatus.Text = "Not logged in";
            }
        }

        protected void GoToMemberPage(object sender, EventArgs e)
        {
            // Redirect to Login page if not logged in
            if (Session["LoggedInUser"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            else
            {
                Response.Redirect("Member.aspx");
            }
        }

        protected void GoToStaffPage(object sender, EventArgs e)
        {
            // Redirect to Login page if not logged in as staff
            if (Session["StaffUser"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            else
            {
                Response.Redirect("Staff.aspx");
            }
        }
    }
}