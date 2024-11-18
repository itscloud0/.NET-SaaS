<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ActivityLog.ascx.cs" Inherits="Application.ActivityLog" %>

<!-- Developed by Ilia Sorokin-->
<div>
    <h3>User Activity Log</h3>
    
    <!-- List to display user activities -->
    <asp:ListBox ID="lstActivityLog" runat="server" Height="200px" Width="400px"></asp:ListBox>
    
    <!-- Button to clear the log -->
    <asp:Button ID="btnClearLog" runat="server" Text="Clear Log" OnClick="ClearLog_Click" />
</div>
