<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Application.Login" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login - Travel Booking Application</title>
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
            width: 90%; /* Adjusted width to make the text box slightly smaller */
            padding: 10px;
            margin: 10px 0;
            border: 1px solid #ddd;
            border-radius: 5px;
        }
        .button {
            padding: 10px 20px;
            color: #fff;
            background-color: #4a90e2;
            border: none;
            border-radius: 5px;
            cursor: pointer;
            width: 100%;
            margin-bottom: 10px;
        }
        .button:hover {
            background-color: #357ab8;
        }
        .error-message {
            color: red;
            margin-top: 10px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h2>Login</h2>
            <asp:TextBox ID="txtUsername" runat="server" CssClass="input-field" Placeholder="Username"></asp:TextBox>
            <asp:TextBox ID="txtPassword" runat="server" CssClass="input-field" TextMode="Password" Placeholder="Password"></asp:TextBox>
            <asp:Button ID="btnLogin" runat="server" CssClass="button" Text="Login" OnClick="btnLogin_Click" />
            <asp:Button ID="btnBack" runat="server" CssClass="button" Text="Back" OnClick="btnBack_Click" />
            <asp:Label ID="lblError" runat="server" CssClass="error-message" />
        </div>
    </form>
</body>
</html>