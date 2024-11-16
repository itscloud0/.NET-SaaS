<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Admin.aspx.cs" Inherits="Application.Admin" %>
<%@ Register Src="~/DeleteConfirmation.ascx" TagPrefix="uc" TagName="DeleteConfirmation" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Admin - Manage Staff Members</title>
    <style>
        /* Main Page Styles */
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

        /* Modal Styles */
        .modal {
            display: none;
            position: fixed;
            z-index: 1;
            left: 0;
            top: 0;
            width: 100%;
            height: 100%;
            overflow: auto;
            background-color: rgba(0, 0, 0, 0.4);
            padding-top: 60px;
        }
        .modal-content {
            background-color: #fefefe;
            margin: auto;
            padding: 20px;
            border: 1px solid #888;
            width: 80%;
            max-width: 400px;
            border-radius: 8px;
            text-align: center;
        }
        .close {
            color: #aaa;
            float: right;
            font-size: 28px;
            font-weight: bold;
        }
        .close:hover,
        .close:focus {
            color: black;
            text-decoration: none;
            cursor: pointer;
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

            <!-- Button to Open Modal -->
            <asp:Button ID="btnShowDelete" runat="server" CssClass="button" Text="Delete Staff Member" OnClientClick="openModal(); return false;" />

            <!-- Modal Structure -->
            <div id="deleteModal" class="modal">
                <div class="modal-content">
                    <span class="close" onclick="closeModal()">&times;</span>
                    <h3>Delete Staff Member</h3>
                    <uc:DeleteConfirmation ID="DeleteConfirmationControl" runat="server" />
                </div>
            </div>

            <!-- Navigation Buttons -->
            <asp:Button ID="btnLogOut" runat="server" CssClass="button" Text="Log Out" OnClick="btnLogOut_Click" /><br />
            <asp:Button ID="btnHomePage" runat="server" CssClass="button" Text="Go to Home Page" OnClick="btnGoToDefault_Click" /><br />
            <asp:Button ID="btnViewAllStaff" runat="server" CssClass="button" Text="View All Staff Members" OnClick="btnViewAllStaff_Click" />

            <!-- List of all staff members -->
            <div class="staff-list">
                <asp:Label ID="lblStaffList" runat="server" Text="" />
            </div>
        </div>
    </form>

    <script type="text/javascript">
        function openModal() {
            document.getElementById("deleteModal").style.display = "block";
        }
        function closeModal() {
            document.getElementById("deleteModal").style.display = "none";
        }
        window.onclick = function (event) {
            var modal = document.getElementById("deleteModal");
            if (event.target == modal) {
                closeModal();
            }
        };
    </script>
</body>
</html>