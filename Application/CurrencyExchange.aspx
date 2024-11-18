<%@ Page Language="C#" Async="true" AutoEventWireup="true" CodeBehind="CurrencyExchange.aspx.cs" Inherits="Application.CurrencyExchange" %>
<!--Developed by Chris Harris-->
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Currency Exchange</title>
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
        .result {
            color: green;
            margin-top: 15px;
        }
        .error {
            color: red;
            margin-top: 15px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h2>Currency Exchange Rate</h2>
            <asp:TextBox ID="txtBaseCurrency" runat="server" CssClass="input-field" Placeholder="Enter Base Currency (e.g., USD)"></asp:TextBox>
            <asp:TextBox ID="txtTargetCurrency" runat="server" CssClass="input-field" Placeholder="Enter Target Currency (e.g., EUR)"></asp:TextBox>
            <asp:Button ID="btnGetRate" runat="server" CssClass="button" Text="Get Exchange Rate" OnClick="btnGetRate_Click" />
            <asp:Label ID="lblResult" runat="server" CssClass="result" Text=""></asp:Label>
            <asp:Label ID="lblError" runat="server" CssClass="error" Text=""></asp:Label>
            <br />
            <!-- Back Button -->
            <asp:Button ID="btnBack" runat="server" CssClass="button" Text="Back to Home" OnClick="btnBack_Click" />
        </div>
    </form>
</body>
</html>
