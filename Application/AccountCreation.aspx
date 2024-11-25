<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AccountCreation.aspx.cs" Inherits="Application.AccountCreation" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Create Member Account</title>
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
        .success-message {
            color: green;
            margin-top: 10px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h2>Create Account</h2>

            <!-- Username input field -->
            <asp:TextBox ID="txtUsername" runat="server" CssClass="input-field" Placeholder="Enter Username"></asp:TextBox>

            <!-- Password input field -->
            <asp:TextBox ID="txtPassword" runat="server" CssClass="input-field" TextMode="Password" Placeholder="Enter Password"></asp:TextBox>

            <!-- CAPTCHA Section -->
            <div class="captcha-container">
                <asp:Image ID="imgCaptcha" runat="server" CssClass="captcha-image" />
                <asp:Button ID="btnRefreshCaptcha" runat="server" CssClass="button" Text="Refresh" OnClick="btnRefreshCaptcha_Click" />
            </div>
            <asp:TextBox ID="txtCaptchaInput" runat="server" CssClass="input-field" Placeholder="Enter CAPTCHA"></asp:TextBox>

            <!-- Create Account button -->
            <asp:Button ID="btnCreateAccount" runat="server" CssClass="button" Text="Create Account" OnClick="btnCreateAccount_Click" />

            <!-- Back button -->
            <asp:Button ID="btnBack" runat="server" CssClass="button" Text="Back to Login" OnClick="btnBack_Click" />

            <!-- Labels for error and success messages -->
            <asp:Label ID="lblMessage" runat="server" CssClass="error-message" />
        </div>
    </form>
</body>
</html>