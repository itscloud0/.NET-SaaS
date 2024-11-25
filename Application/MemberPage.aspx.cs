using System;
using System.Web.UI;

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
    }
}