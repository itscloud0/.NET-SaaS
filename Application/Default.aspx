<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Application.Default" %>
<!-- Developed by Ilia Sorokin, Chris Harris and Cole Eastman-->
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <title>Welcome to Travel Booking Application</title>
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
        h1, h2 {
            color: #4a90e2;
        }
        .description {
            font-size: 1.1em;
            color: #333;
            margin-bottom: 20px;
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
        table {
            width: 100%;
            margin-top: 20px;
            border-collapse: collapse;
            table-layout: fixed; /* Ensures the table fits within the container */
        }
        table, th, td {
            border: 1px solid #ddd;
        }
        th, td {
            padding: 12px;
            text-align: center;
            word-wrap: break-word; /* Wraps long text within cells */
        }
        th {
            background-color: #4a90e2;
            color: white;
        }
        tr:nth-child(even) {
            background-color: #f2f2f2;
        }
        .status-label {
            display: block;
            margin: 15px 0;
            font-size: 1.1em;
            color: #333;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <!-- Title and Application Introduction -->
            <h1>Welcome to Our Travel Booking Application</h1>
            <p class="description">
                This application offers users the ability to browse and book travel options, including flights, hotels, and activities.
                Public visitors can explore basic features, while registered members gain access to additional booking options.
                Staff can manage bookings and maintain the system. New users can register to unlock full member access.
            </p>
            
            <!-- Instructions for TA/Grader -->
            <h2>Instructions for Testing</h2>
            <ul>
                <li>To access the "Member" page, you can register as a new user.</li>
                <li>To test the "Staff" page, use the provided staff login: <strong>Username: "TA", Password: "Cse445!"</strong></li>
                <li>Make sure to log out before switching between member and staff roles to test redirection properly.</li>
            </ul>

            <!-- Buttons for Member and Staff Pages with Login Redirection -->
            <div>
                <asp:Button ID="btnMemberPage" CssClass="button" runat="server" Text="Member Page" OnClick="GoToMemberPage" />
                <asp:Button ID="btnStaffPage" CssClass="button" runat="server" Text="Staff Page" OnClick="GoToStaffPage" />
            </div>

            <!-- Service Directory Table -->
            <h2>Service Directory</h2>
            <table>
                <tr>
                    <th>Provider Name</th>
                    <th>Component Type</th>
                    <th>Operation Name</th>
                    <th>Parameters</th>
                    <th>Return Type</th>
                    <th>Description</th>
                    <th>TryIt Link</th>
                </tr>
                <tr>
                    <td>Ilia Sorokin</td>
                    <td>WSDL Service</td>
                    <td>SearchHotels</td>
                    <td>city (string), startDate (DateTime), endDate (DateTime)</td>
                    <td>List&lt;Hotel&gt;</td>
                    <td>Searches for available hotels in a specified city for given dates</td>
                    <td><a href="Services/BookHotel.aspx" class="button">TryIt</a></td>
                </tr>
                <tr>
                    <td>Ilia Sorokin</td>
                    <td>WSDL Service</td>
                    <td>BookHotel</td>
                    <td>hotelName (string), startDate (DateTime), endDate (DateTime), customerName (string)</td>
                    <td>String</td>
                    <td>Books a hotel room for a customer within specified dates</td>
                    <td><a href="Services/BookHotel.aspx" class="button">TryIt</a></td>
                </tr>
                <tr>
                    <td>Ilia Sorokin</td>
                    <td>User Control</td>
                    <td>AddActivity, LoadActivityLog, SaveActivityLog, ClearLog, LogFlightBooking, LogHotelBooking, LogLogin, LogLogout</td>
                    <td>username (string),hotelName (string), checkInDate (string), checkOutDate (string), time (string, depart (string), activity(string)</td>
                    <td>void</td>
                    <td>Logs the activity of the user.</td>
                    <td><a href="LocalComponents/ActivityLogPage.aspx" class="button">TryIt</a></td>
                </tr>
                <tr>
                    <td>Ilia Sorokin</td>
                    <td>DLL Class Library Module</td>
                    <td>Encrypt, Decrypt</td>
                    <td>plainText (string), ecryptedText (string)</td>
                    <td>String</td>
                    <td>Encrypts/Decrypts a string with a specific key value. Used to securely store login credentials and names for the reservations.</td>
                    <td><a href="LocalComponents/EncryptValues.aspx" class="button">TryIt</a></td>
                </tr>
                    <tr>
                    <td>Cole Eastman</td>
                    <td>WSDL Service</td>
                    <td>BookFlightFunction</td>
                    <td>BookFlightFunction (int, string, string)</td>
                    <td>String</td>
                    <td>Books a flight</td>
                    <td><a href="Services/BookFlight.aspx" class="button">TryIt</a></td>
                </tr>
                <tr>
                    <td>Cole Eastman</td>
                    <td>Global.asax</td>
                    <td>Session_Start, Session_End</td>
                    <td>None</td>
                    <td>Integer</td>
                    <td>Displays the current count of active users</td>
                    <td><a href="LocalComponents/UserCount.aspx" class="button">TryIt</a></td>
                </tr>
                <tr>
                    <td>Chris Harris</td>
                    <td>User Control</td>
                    <td>Login Window</td>
                    <td>Username, Password</td>
                    <td>String</td>
                    <td>Login page for user authentication</td>
                    <td><a href="Login.aspx" class="button">TryIt</a></td>
                </tr>
                <tr>
                    <td>Chris Harris</td>
                    <td>Cookie for storing user profile</td>
                    <td>User Profile Management</td>
                    <td>User ID</td>
                    <td>String</td>
                    <td>Page for managing user profile and temporary states using cookies and sessions</td>
                    <td><a href="LocalComponents/UserProfile.aspx" class="button">TryIt</a></td>
                </tr>
                <tr>
                    <td>Chris Harris</td>
                    <td>WSDL</td>
                    <td>Exchange Rate Retrieval</td>
                    <td>Base Currency, Target Currency</td>
                    <td>Decimal</td>
                    <td>Page for retrieving and displaying currency exchange rates</td>
                    <td><a href="CurrencyExchange.aspx" class="button">TryIt</a></td>
                </tr>
                <!-- Add additional rows for other services as needed -->
            </table>

            <!-- Display login status -->
            <asp:Label ID="lblStatus" CssClass="status-label" runat="server" Text="Not logged in" />
        </div>
    </form>
</body>
</html>