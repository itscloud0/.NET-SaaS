<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BookFlight.aspx.cs" Inherits="Application.BookFlight1" %> 

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Book Flight - Travel Booking Application</title>
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
            <h2>Book Flight</h2>
            <asp:TextBox ID="txtTime" runat="server" CssClass="input-field" Placeholder="Select Time" TextMode="Time"></asp:TextBox>
            <asp:TextBox ID="txtDepart" runat="server" CssClass="input-field" MaxLength="3" Placeholder="From (EX: LAX)"></asp:TextBox>
            <asp:TextBox ID="txtArrival" runat="server" CssClass="input-field" MaxLength="3" Placeholder="To (EX: ATL)"></asp:TextBox>
            <asp:Button ID="btnSubmit" runat="server" CssClass="button" Text="Book Flight" OnClick="btnSubmit_Click" />
            <asp:Label ID="lblResult" runat="server" CssClass="result-message" />
        </div>
    </form>
</body>
</html>
