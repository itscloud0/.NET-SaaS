using System;
using System.Web.UI;

namespace Application
{
    public partial class Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                UpdateLoginStatus();
            }
        }

        protected void GoToMemberPage(object sender, EventArgs e)
        {
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
            if (Session["StaffUser"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            else
            {
                Response.Redirect("Admin.aspx");
            }
        }
    }
}