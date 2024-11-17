// Developed by Ilia Sorokin
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Application
{
    public partial class BookHotel : System.Web.UI.Page
    {
        // Instance of the HotelBookingService for handling booking operations
        private HotelBookingService bookingService = new HotelBookingService();

        // Event handler for page load
        protected void Page_Load(object sender, EventArgs e)
        {
            // Disable unobtrusive validation to avoid conflicts with modern validation settings
            ValidationSettings.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;

            if (!IsPostBack)
            {
                // Set the minimum allowed date for date pickers to today's date
                txtStartDate.Attributes["min"] = DateTime.Today.ToString("yyyy-MM-dd");
                txtEndDate.Attributes["min"] = DateTime.Today.ToString("yyyy-MM-dd");
            }
        }

        // Event handler for the "Search" button click
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                // Ensure all required fields are filled
                if (string.IsNullOrEmpty(txtCity.Text) ||
                    string.IsNullOrEmpty(txtStartDate.Text) ||
                    string.IsNullOrEmpty(txtEndDate.Text))
                {
                    lblError.Text = "Please fill in all required fields.";
                    return;
                }

                // Parse input dates
                DateTime startDate = DateTime.Parse(txtStartDate.Text);
                DateTime endDate = DateTime.Parse(txtEndDate.Text);

                // Validate the input dates
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

                // Call the service to search for hotels
                List<Hotel> availableHotels = bookingService.SearchHotels(txtCity.Text, startDate, endDate);

                if (availableHotels.Count == 0)
                {
                    lblError.Text = "No available hotels found for the selected dates.";
                }
                else
                {
                    lblError.Text = string.Empty; // Clear any error messages
                }

                // Bind the search results to the Repeater for display
                rptHotels.DataSource = availableHotels;
                rptHotels.DataBind();

                // Store the search dates in ViewState for later use
                ViewState["StartDate"] = startDate;
                ViewState["EndDate"] = endDate;
            }
            catch (Exception ex)
            {
                lblError.Text = "Error: " + ex.Message; // Display any errors encountered
            }
        }

        // Event handler for Repeater item commands (e.g., booking a hotel)
        protected void rptHotels_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "BookHotel")
            {
                // Show the booking panel and store the selected hotel's name
                string selectedHotel = e.CommandArgument.ToString();
                lblSelectedHotel.Text = selectedHotel;
                ViewState["SelectedHotel"] = selectedHotel;
                pnlBooking.Visible = true; // Display the booking panel
            }
        }

        // Event handler for the "Confirm Booking" button click
        protected void btnConfirmBooking_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate that the customer name is entered
                if (string.IsNullOrEmpty(txtCustomerName.Text))
                {
                    lblError.Text = "Please enter your name.";
                    return;
                }

                // Retrieve data from ViewState
                string selectedHotel = ViewState["SelectedHotel"].ToString();
                DateTime startDate = (DateTime)ViewState["StartDate"];
                DateTime endDate = (DateTime)ViewState["EndDate"];

                // Confirm the booking using the service
                string result = bookingService.BookHotel(
                    selectedHotel,
                    startDate,
                    endDate,
                    txtCustomerName.Text
                );

                // Display confirmation message if the booking is successful
                if (!string.IsNullOrEmpty(result))
                {
                    lblError.Text = "Your booking has been confirmed for " + selectedHotel +
                                    " from " + startDate.ToShortDateString() +
                                    " to " + endDate.ToShortDateString();
                    pnlBooking.Visible = false; // Hide the booking panel
                }

                // Clear input fields
                txtCustomerName.Text = string.Empty;

                // Optionally refresh the hotel list
                btnSearch_Click(sender, e);
            }
            catch (Exception ex)
            {
                lblError.Text = "Booking Error: " + ex.Message; // Display booking errors
            }
        }

        // Event handler for the "Cancel Booking" button click
        protected void btnCancelBooking_Click(object sender, EventArgs e)
        {
            // Hide the booking panel and clear customer name input
            pnlBooking.Visible = false;
            txtCustomerName.Text = string.Empty;
        }
    }
}
