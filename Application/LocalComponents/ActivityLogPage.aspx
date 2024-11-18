<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ActivityLogPage.aspx.cs" Inherits="Application.ActivityLogPage" %>
<%@ Register Src="~/ActivityLog.ascx" TagPrefix="uc" TagName="ActivityLog" %>

<!-- Developed by Ilia Sorokin -->
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Activity Log</title>
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
        .status-label {
            margin-top: 20px;
            font-size: 1.1em;
            color: #333;
        }
        .log-container {
            margin-top: 20px;
        }
        .log-entry {
            padding: 8px;
            border-bottom: 1px solid #ddd;
        }
        .log-entry:last-child {
            border-bottom: none;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h1>Activity Log</h1>

            <div class="log-container">
                <h2>Current Log</h2>
                <asp:ListBox ID="lstActivityLog" runat="server" Height="200px" Width="100%"></asp:ListBox>
            </div>

            <div>
                <!-- Corrected OnClick event handler reference -->
                <asp:Button ID="btnClearLog" CssClass="button" runat="server" Text="Clear Log" OnClick="ClearLog_Click" />
            </div>

            <div class="status-label">
                <asp:Label ID="lblStatus" runat="server" Text="Log displayed above. Click 'Clear Log' to reset." />
            </div>
        </div>
    </form>
</body>
</html>
