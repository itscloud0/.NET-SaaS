using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Application
{
    public partial class BookHotel : System.Web.UI.Page
    {
        private HotelBookingService bookingService = new HotelBookingService();

        protected void Page_Load(object sender, EventArgs e)
        {
            // Disable unobtrusive validation
            ValidationSettings.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;

            if (!IsPostBack)
            {
                // Set minimum date to today for date pickers
                txtStartDate.Attributes["min"] = DateTime.Today.ToString("yyyy-MM-dd");
                txtEndDate.Attributes["min"] = DateTime.Today.ToString("yyyy-MM-dd");
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate required fields
                if (string.IsNullOrEmpty(txtCity.Text) ||
                    string.IsNullOrEmpty(txtStartDate.Text) ||
                    string.IsNullOrEmpty(txtEndDate.Text))
                {
                    lblError.Text = "Please fill in all required fields.";
                    return;
                }

                DateTime startDate = DateTime.Parse(txtStartDate.Text);
                DateTime endDate = DateTime.Parse(txtEndDate.Text);

                // Validate dates
                if (startDate < DateTime.Today)
                {
                    lblError.Text = "Check-in date cannot be in the past.";
                    return;
                }

                if (endDate <= startDate)
                {
                    lblError.Text = "Check-out date must be after check-in date.";
                    return;
                }

                // Search for available hotels
                List<Hotel> availableHotels = bookingService.SearchHotels(txtCity.Text, startDate, endDate);

                if (availableHotels.Count == 0)
                {
                    lblError.Text = "No available hotels found for the selected dates.";
                }
                else
                {
                    lblError.Text = string.Empty;
                }

                // Bind the results to the repeater
                rptHotels.DataSource = availableHotels;
                rptHotels.DataBind();

                // Store search dates in ViewState for booking
                ViewState["StartDate"] = startDate;
                ViewState["EndDate"] = endDate;
            }
            catch (Exception ex)
            {
                lblError.Text = "Error: " + ex.Message;
            }
        }

        protected void rptHotels_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "BookHotel")
            {
                // Show booking panel and store selected hotel
                string selectedHotel = e.CommandArgument.ToString();
                lblSelectedHotel.Text = selectedHotel;
                ViewState["SelectedHotel"] = selectedHotel;
                pnlBooking.Visible = true;
            }
        }

        protected void btnConfirmBooking_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate customer name input
                if (string.IsNullOrEmpty(txtCustomerName.Text))
                {
                    lblError.Text = "Please enter your name.";  // Show error if name is not entered
                    return;  // Prevent further execution and stop page reload
                }

                // Extract the necessary data from ViewState
                string selectedHotel = ViewState["SelectedHotel"].ToString();
                DateTime startDate = (DateTime)ViewState["StartDate"];
                DateTime endDate = (DateTime)ViewState["EndDate"];

                // Call the service to confirm the booking
                string result = bookingService.BookHotel(
                    selectedHotel,
                    startDate,
                    endDate,
                    txtCustomerName.Text
                );

                // Check the result from booking
                if (!string.IsNullOrEmpty(result))
                {
                    // Show confirmation message
                    lblError.Text = "Your booking has been confirmed for " + selectedHotel + " from " + startDate.ToShortDateString() + " to " + endDate.ToShortDateString();
                    pnlBooking.Visible = false;  // Hide the booking panel after confirmation
                }

                // Clear the form inputs
                txtCustomerName.Text = string.Empty;

                // Optionally, refresh the hotel list if needed
                btnSearch_Click(sender, e);
            }
            catch (Exception ex)
            {
                lblError.Text = "Booking Error: " + ex.Message;  // Display any exception message
            }
        }

        protected void btnCancelBooking_Click(object sender, EventArgs e)
        {
            pnlBooking.Visible = false;
            txtCustomerName.Text = string.Empty;
        }
    }
}
