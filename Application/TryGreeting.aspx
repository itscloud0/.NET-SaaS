<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TryGreeting.aspx.cs" Inherits="Application.TryGreeting" %>
<%@ Register Src="GreetingControl.ascx" TagName="GreetingControl" TagPrefix="uc" %>
<!DOCTYPE html>
<!-- Developed by Cole Eastman-->
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Try Greeting Control</title>
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
            table-layout: fixed;
        }
        table, th, td {
            border: 1px solid #ddd;
        }
        th, td {
            padding: 12px;
            text-align: center;
            word-wrap: break-word;
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
            <h1>Try Greeting Control</h1>
            <p class="description">
                This page demonstrates the Greeting Control component, which dynamically generates a personalized greeting based on the current time of day. The user's session information is retrieved using session tokens to determine if the user is logged in and display their username or a default greeting for guests.
            </p>
            <uc:GreetingControl ID="GreetingControl1" runat="server" />
        </div>
    </form>
</body>
</html>
