<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Admin.aspx.cs" Inherits="Application.Admin" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Admin - Manage Staff Members</title>
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
        .message {
            color: red;
            margin-top: 10px;
        }
        .staff-list {
            text-align: left;
            margin-top: 20px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h2>Admin - Manage Staff Members</h2>
            <asp:Label ID="lblMessage" runat="server" CssClass="message" Text=""></asp:Label><br /><br />

            <!-- Add Staff Member Section -->
            <asp:TextBox ID="txtUsername" runat="server" CssClass="input-field" Placeholder="Username"></asp:TextBox><br />
            <asp:TextBox ID="txtPassword" runat="server" CssClass="input-field" TextMode="Password" Placeholder="Password"></asp:TextBox><br />
            <asp:Button ID="btnAddStaff" runat="server" CssClass="button" Text="Add Staff" OnClick="btnAddStaff_Click" /><br />

            <!-- Show Delete Staff Member Text Box Button -->
            <asp:Button ID="btnShowDelete" runat="server" CssClass="button" Text="Delete Staff Member" OnClick="btnShowDelete_Click" /><br />

            <!-- Delete Staff Member Panel (Initially Hidden) -->
            <asp:Panel ID="pnlDeleteStaff" runat="server" Visible="false">
                <asp:TextBox ID="txtDeleteUsername" runat="server" CssClass="input-field" Placeholder="Enter Username to Delete"></asp:TextBox><br />
                <asp:Button ID="btnDeleteStaff" runat="server" CssClass="button" Text="Confirm Delete" OnClick="btnDeleteStaff_Click" /><br />
            </asp:Panel>

            <!-- Navigation Buttons -->
            <asp:Button ID="btnBackToLogin" runat="server" CssClass="button" Text="Back to Login" OnClick="btnBackToLogin_Click" /><br />
            <asp:Button ID="btnViewAllStaff" runat="server" CssClass="button" Text="View All Staff Members" OnClick="btnViewAllStaff_Click" />

            <!-- List of all staff members -->
            <div class="staff-list">
                <asp:Label ID="lblStaffList" runat="server" Text="" />
            </div>
        </div>
    </form>
</body>
</html>