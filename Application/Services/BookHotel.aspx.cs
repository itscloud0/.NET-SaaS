using System;
using System.Web.UI;
using Application;

namespace Application
{
    public partial class BookHotel : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                // Create client to call the HotelBookingService
                HotelBookingServiceClient client = new HotelBookingServiceClient();

                string city = txtCity.Text;
                DateTime date = DateTime.Parse(txtDate.Text);

                // Call the service method
                string result = client.BookHotel(city, date);

                lblResult.Text = result;

                client.Close();
            }
            catch (Exception ex)
            {
                lblResult.Text = "Error: " + ex.Message;
            }
        }
    }
}
