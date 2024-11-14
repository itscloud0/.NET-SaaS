using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Application.BookFlightServiceReference;  // Make sure to add your service reference namespace

namespace Application
{
    public partial class BookFlight1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtTime.Attributes.Add("step", "1800"); // Sets the time increment to 30 minutes
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                // Create a client to call the WCF service
                BookFlightClient client = new BookFlightClient();

                // Retrieve input values from text boxes
                string timeText = txtTime.Text;  // This will be in "HH:mm" format
                string depart = txtDepart.Text;
                string arrival = txtArrival.Text;

                // Convert the "HH:mm" time format to an integer hour value
                int hours = ParseTimeToHours(timeText);

                // Call the BookFlightFunction method in the WCF service with the converted hour
                string result = client.BookFlightFunction(hours, depart, arrival);

                // Display the result in lblResult
                lblResult.Text = result;

                // Close the client connection
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
            return int.Parse(timeParts[0]); // Only take hours
        }
    }
}
