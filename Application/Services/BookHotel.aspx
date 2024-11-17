<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BookHotel.aspx.cs" Inherits="Application.BookHotel" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Book Hotel</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            background-color: #f0f2f5;
            margin: 0;
            padding: 0;
        }
        .container {
            max-width: 800px;
            margin: auto;
            padding: 20px;
            background-color: #ffffff;
            border-radius: 8px;
            box-shadow: 0px 4px 8px rgba(0, 0, 0, 0.2);
            overflow: hidden;
        }
        h1 {
            color: #4a90e2;
        }
        .description {
            font-size: 1.1em;
            color: #333;
            margin-bottom: 20px;
        }
        .form-group {
            margin-bottom: 20px;
        }
        .form-group label {
            font-size: 1.1em;
            color: #333;
        }
        .form-group input {
            width: 100%;
            padding: 10px;
            font-size: 1em;
            margin-top: 5px;
            border-radius: 5px;
            border: 1px solid #ccc;
        }
        .button-container {
            margin-top: 20px;
        }
        .button {
            display: inline-block;
            padding: 10px 20px;
            color: #fff;
            background-color: #4a90e2;
            border: none;
            border-radius: 5px;
            cursor: pointer;
            text-decoration: none;
        }
        .button:hover {
            background-color: #357ab8;
        }
        .hotel-card {
            border: 1px solid #ddd;
            padding: 20px;
            margin-top: 10px;
            border-radius: 8px;
            background-color: #f9f9f9;
        }
        .hotel-card h3 {
            color: #4a90e2;
        }
        .hotel-card p {
            color: #333;
        }
        .error-message {
            color: red;
            font-size: 1.1em;
            margin-top: 10px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h1>Book a Hotel</h1>
            <p class="description">
                Search and book hotels for your next stay. Enter your preferred city and dates to find available accommodations.
            </p>
            
            <div class="form-group">
                <label for="txtCity">City:</label>
                <asp:TextBox ID="txtCity" runat="server"></asp:TextBox>
            </div>

            <div class="form-group">
                <label for="txtStartDate">Check-in Date:</label>
                <asp:TextBox ID="txtStartDate" runat="server" TextMode="Date"></asp:TextBox>
            </div>

            <div class="form-group">
                <label for="txtEndDate">Check-out Date:</label>
                <asp:TextBox ID="txtEndDate" runat="server" TextMode="Date"></asp:TextBox>
            </div>

            <div class="button-container">
                <asp:Button ID="btnSearch" runat="server" Text="Search Hotels" OnClick="btnSearch_Click" CssClass="button" />
            </div>
            
            <asp:Label ID="lblError" runat="server" CssClass="error-message"></asp:Label>

            <asp:Repeater ID="rptHotels" runat="server" OnItemCommand="rptHotels_ItemCommand">
                <ItemTemplate>
                    <div class="hotel-card">
                        <h3><%# Eval("Name") %></h3>
                        <p><strong>Rating:</strong> <%# Eval("Rating") %></p>
                        <p><strong>Address:</strong> 
                            <%# Eval("Address.Number") %> <%# Eval("Address.Street") %>,
                            <%# Eval("Address.City") %>, <%# Eval("Address.State") %> <%# Eval("Address.Zip") %>
                        </p>
                        <p><strong>Nearest Airport:</strong> <%# Eval("Address.NearstAirport") %></p>
                        <p><strong>Phone:</strong> <%# string.Join(", ", (List<string>)Eval("Phone")) %></p>
                        
                        <asp:Button ID="btnBook" runat="server" 
                            Text="Book Now" 
                            CommandName="BookHotel" 
                            CommandArgument='<%# Eval("Name") %>' 
                            CssClass="button" />
                    </div>
                </ItemTemplate>
            </asp:Repeater>

            <asp:Panel ID="pnlBooking" runat="server" Visible="false">
                <h3>Complete Your Booking</h3>
                <p>Hotel: <asp:Label ID="lblSelectedHotel" runat="server"></asp:Label></p>
                
                <div class="form-group">
                    <label for="txtCustomerName">Your Name:</label>
                    <asp:TextBox ID="txtCustomerName" runat="server"></asp:TextBox>
                </div>

                <div class="button-container">
                    <asp:Button ID="btnConfirmBooking" runat="server" Text="Confirm Booking" 
                        OnClick="btnConfirmBooking_Click" CssClass="button" />
                    <asp:Button ID="btnCancelBooking" runat="server" Text="Cancel" 
                        OnClick="btnCancelBooking_Click" CssClass="button" />
                </div>
            </asp:Panel>
        </div>
    </form>
</body>
</html>
