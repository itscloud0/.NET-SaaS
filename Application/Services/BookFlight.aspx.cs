using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Application.BookFlightServiceReference;

namespace Application.Services
{
    public partial class BookFlight1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                // client to call the WCF service
                BookFlightClient client = new BookFlightClient();

                string timeText = txtTime.Text;
                string depart = txtDepart.Text;
                string arrival = txtArrival.Text;

                int hours = ParseTimeToHours(timeText);

                string result = client.BookFlightFunction(hours, depart, arrival);

                lblResult.Text = result;

                client.Close();
            }
            catch (Exception ex)
            {
                lblResult.Text = "Error: " + ex.Message;
            }
        }

        private int ParseTimeToHours(string timeText)
        {
            var timeParts = timeText.Split(':');
            return int.Parse(timeParts[0]);
        }
    }
}
