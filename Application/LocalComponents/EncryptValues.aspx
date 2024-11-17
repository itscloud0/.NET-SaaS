<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EncryptValues.aspx.cs" Inherits="Application.EncryptValues" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Encryption/Decryption Tool</title>
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
            margin-top: 50px;
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
            width: 100%;
            padding: 10px;
            margin-bottom: 15px;
            border: 1px solid #ddd;
            border-radius: 5px;
            box-sizing: border-box;
            font-size: 14px;
        }
        .input-field::placeholder {
            font-style: italic;
            color: #999;
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
            margin-bottom: 10px;
            text-align: left;
        }
        .result-box {
            background-color: #f8f9fa;
            border: 1px solid #ddd;
            border-radius: 5px;
            padding: 10px;
            margin-top: 15px;
            text-align: left;
            word-break: break-all;
        }
        .result-label {
            font-weight: bold;
            color: #4a90e2;
            margin-bottom: 5px;
            display: block;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h2>Encryption/Decryption Tool</h2>
            
            <asp:TextBox ID="txtInput" runat="server" CssClass="input-field" placeholder="Enter text to encrypt/decrypt" />
            
            <asp:Button ID="btnEncrypt" runat="server" Text="Encrypt" CssClass="button" OnClick="btnEncrypt_Click" />
            <asp:Button ID="btnDecrypt" runat="server" Text="Decrypt" CssClass="button" OnClick="btnDecrypt_Click" />
            
            <asp:Label ID="lblMessage" runat="server" CssClass="message" />

            <asp:Panel ID="pnlResults" runat="server" Visible="false">
                <div class="result-box">
                    <span class="result-label">Input Text:</span>
                    <asp:Label ID="lblInputText" runat="server" />
                </div>
                <div class="result-box">
                    <span class="result-label">Result:</span>
                    <asp:Label ID="lblResult" runat="server" />
                </div>
            </asp:Panel>
        </div>
    </form>
</body>
</html>