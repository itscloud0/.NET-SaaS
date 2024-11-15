<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BookHotel.aspx.cs" Inherits="Application.BookHotel" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Book Hotel - Hotel Booking Application</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            background-color: #f0f2f5;
            margin: 0;
            padding: 0;
        }
        .container {
            max-width: 400px;
            margin: auto;
            padding: 20px;
            background-color: #ffffff;
            border-radius: 8px;
            box-shadow: 0px 4px 8px rgba(0, 0, 0, 0.2);
            text-align: center;
        }
        h2 {
            color: #4a90e2;
        }
        .input-field {
            width: 90%;
            padding: 10px;
            margin: 10px 0;
            border: 1px solid #ddd;
            border-radius: 5px;
            text-transform: uppercase;
        }
        .button {
            padding: 10px 20px;
            color: #fff;
            background-color: #4a90e2;
            border: none;
            border-radius: 5px;
            cursor: pointer;
            width: 95%;
            margin-bottom: 10px;
        }
        .button:hover {
            background-color: #357ab8;
        }
        .result-message {
            color: green;
            margin-top: 10px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h2>Book Hotel</h2>
            <asp:TextBox ID="txtCity" runat="server" CssClass="input-field" Placeholder="Enter City" />
            <asp:TextBox ID="txtDate" runat="server" CssClass="input-field" TextMode="Date" Placeholder="Select Date"></asp:TextBox>
            <asp:Button ID="btnSubmit" runat="server" CssClass="button" Text="Book Hotel" OnClick="btnSubmit_Click" />
            <asp:Label ID="lblResult" runat="server" CssClass="result-message" />
        </div>
    </form>
</body>
</html>
