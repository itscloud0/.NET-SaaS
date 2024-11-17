﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Application
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int activeUsers = (int)(Application["ActiveUsers"] ?? 0);

            if (Session["LoggedInUser"] != null)
            {
                lblStatus.Text = $"Active Users: {activeUsers}";
            }
            else
            {
                lblStatus.Text = $"Active Users: {activeUsers}";
            }
        }

    }
}