<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MemberPage.aspx.cs" Inherits="Application.MemberPage" %>
<%@ Register Src="~/ActivityLog.ascx" TagPrefix="uc" TagName="ActivityLog" %>
<!DOCTYPE html>
<!--Developed by Cole Eastman-->
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Member Portal</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            background-color: #f0f2f5;
            margin: 0;
            padding: 0;
        }

        .container {
            max-width: 400px;
            margin: 50px auto; /* Center and add vertical spacing */
            padding: 20px;
            background-color: #ffffff;
            border-radius: 8px;
            box-shadow: 0px 4px 8px rgba(0, 0, 0, 0.2);
            text-align: center;
        }

        h2 {
            color: #4a90e2;
            margin-bottom: 20px; /* Add spacing below the heading */
        }

        .welcome-message {
            font-size: 1.2em;
            margin-bottom: 20px; /* Space between message and buttons */
            color: #333;
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
            display: block;
            width: 100%;
            padding: 10px;
            color: #fff;
            background-color: #4a90e2;
            border: none;
            border-radius: 5px;
            cursor: pointer;
            text-decoration: none;
            margin-bottom: 10px; /* Spacing between buttons */
        }

        .button:hover {
            background-color: #357ab8;
        }

        .error-message, .success-message {
            font-size: 1em;
            margin-bottom: 15px;
        }

        .error-message {
            color: red;
        }

        .success-message {
            color: green;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <uc:ActivityLog ID="activityLog" runat="server" Visible="false"/>
        <div class="container">
            <!-- Page Title -->
            <h2>Member Portal</h2>
            
            <!-- Welcome Message -->
            <asp:Label ID="lblMemberWelcome" runat="server" CssClass="welcome-message" Text="Welcome, Member!" />

            <!-- Error/Success Messages -->
            <asp:Label ID="lblMessage" runat="server" CssClass="error-message" Text="" />

            <!-- Change Password Section -->
            <asp:TextBox ID="txtNewPassword" runat="server" CssClass="input-field" TextMode="Password" Placeholder="Enter New Password" />
            <asp:Button ID="btnChangePassword" runat="server" CssClass="button" Text="Change Password" OnClick="btnChangePassword_Click" />

            <!-- Delete Account Section -->
            <asp:Button ID="btnDeleteAccount" runat="server" CssClass="button" Text="Delete Account" OnClick="btnDeleteAccount_Click" />

            <!-- Logout Button -->
            <asp:Button ID="btnLogout" runat="server" CssClass="button" Text="Logout" OnClick="btnLogout_Click" />
            
            <!-- Back to Home Page Button -->
            <asp:Button ID="btnHomePage" runat="server" CssClass="button" Text="Back to Home" PostBackUrl="Default.aspx" />
        </div>
    </form>
</body>
</html>