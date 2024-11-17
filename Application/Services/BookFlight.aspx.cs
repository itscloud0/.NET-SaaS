// Developed by Cole Eastman
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
        // Event handler for page load
        protected void Page_Load(object sender, EventArgs e)
        {
            // Placeholder for any initialization logic during page load
        }

        // Event handler for the Submit button click
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                // Create a client instance to call the WCF service
                BookFlightClient client = new BookFlightClient();

                // Retrieve input values from text boxes
                string timeText = txtTime.Text;
                string depart = txtDepart.Text;
                string arrival = txtArrival.Text;

                // Parse the time input into hours
                int hours = ParseTimeToHours(timeText);

                // Call the service method with the input values
                string result = client.BookFlightFunction(hours, depart, arrival);

                // Display the result from the service in a label
                lblResult.Text = result;

                // Close the client connection
                client.Close();
            }
            catch (Exception ex)
            {
                // Handle and display any errors that occur during execution
                lblResult.Text = "Error: " + ex.Message;
            }
        }

        // Helper method to parse time in "HH:mm" format to hours
        private int ParseTimeToHours(string timeText)
        {
            // Split the time string by colon and extract the hour component
            var timeParts = timeText.Split(':');
            return int.Parse(timeParts[0]); // Convert the hour component to an integer
        }
    }
}
