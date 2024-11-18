<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserProfile.aspx.cs" Inherits="Application.UserProfile" %>
<!-- Developed by Chris Harris -->
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>User Profile</title>
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
            margin-bottom: 15px;
            border: 1px solid #ddd;
            border-radius: 5px;
            font-size: 14px;
        }


        .button {
            padding: 10px 20px;
            color: #fff;
            background-color: #4a90e2;
            border: none;
            border-radius: 5px;
            cursor: pointer;
            margin-bottom: 15px;
            width: 100%;
        }


        .button:hover {
            background-color: #357ab8;
        }


        .message {
            color: green;
            margin-bottom: 10px;
        }
    </style>
</head>
<body>
    <!-- Form for the user profile section -->
    <form id="form1" runat="server">
        <div class="container">
            <!-- Message label to display status or success messages -->
            <asp:Label ID="lblMessage" runat="server" CssClass="message" Text=""></asp:Label>
            
            <!-- Textbox for entering user name -->
            <asp:TextBox ID="txtName" runat="server" CssClass="input-field" Placeholder="Enter your name"></asp:TextBox>
            
            <!-- Textbox for entering user age -->
            <asp:TextBox ID="txtAge" runat="server" CssClass="input-field" Placeholder="Enter your age"></asp:TextBox>
            
            <!-- Button to save user profile -->
            <asp:Button ID="btnSaveProfile" runat="server" CssClass="button" Text="Save Profile" OnClick="btnSaveProfile_Click" />
            
            <!-- Button to show the saved profile -->
            <asp:Button ID="btnShowProfile" runat="server" CssClass="button" Text="Show Profile" OnClick="btnShowProfile_Click" />
            <br />
            <!-- Back Button -->
            <asp:Button ID="btnBack" runat="server" CssClass="button" Text="Back to Home" OnClick="btnBack_Click" />
        </div>
    </form>
</body>
</html>

