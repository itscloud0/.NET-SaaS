<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DeleteConfirmation.ascx.cs" Inherits="Application.Controls.DeleteConfirmation" %>

<div class="delete-confirmation">
    <asp:Label ID="lblMessage" runat="server" CssClass="message" Text="Enter username to delete:"></asp:Label><br />
    <asp:TextBox ID="txtDeleteUsername" runat="server" CssClass="input-field" Placeholder="Username to Delete"></asp:TextBox><br />

    <!-- Captcha Section -->
    <asp:Label ID="lblCaptcha" runat="server" CssClass="captcha-label"></asp:Label><br />
    <asp:TextBox ID="txtCaptchaInput" runat="server" CssClass="input-field" Placeholder="Enter Captcha"></asp:TextBox><br />

    <asp:Button ID="btnConfirmDelete" runat="server" CssClass="button" Text="Confirm Delete" OnClick="btnConfirmDelete_Click" /><br />
</div>