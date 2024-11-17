<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserCount.aspx.cs" Inherits="Application.WebForm1" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Display Active User Count</title>
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
            <h2>Active User Count</h2>
            <asp:Label ID="lblStatus" CssClass="status-label" runat="server" Text="Not logged in" />
            <p>Open another web browser (e.g. Firefox, Safari, Google Chrome Incognito)</p>
            <p>Open this link to increment the active user count.</p>
            <p>Refresh the page if the update is not being reflected.</p>
            <p>The active user count will only decrement after 1 minute of inactivity</p>
        </div>
    </form>
</body>
</html>
