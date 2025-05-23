﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

// Created by Cole Eastman

namespace Application
{
    public partial class GreetingControl : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int currentHour = DateTime.Now.Hour;

                string username = "Guest"; // Default value
                if (Session["StaffUser"] != null)
                {
                    username = Session["StaffUser"].ToString();
                }
                else if (Session["MemberUser"] != null)
                {
                    username = Session["MemberUser"].ToString();
                }

                // Greeting based on the time of day
                string greeting;
                if (currentHour < 12)
                {
                    greeting = "Good Morning";
                }
                else if (currentHour < 17) // changed from 18 to 17 due to the time of year
                {
                    greeting = "Good Afternoon";
                }
                else
                {
                    greeting = "Good Evening";
                }

                lblGreeting.Text = $"{greeting}, {username}!";
            }
        }
    }
}
