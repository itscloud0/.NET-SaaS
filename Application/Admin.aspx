<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Admin.aspx.cs" Inherits="Application.Admin" %>
<%@ Register Src="~/DeleteConfirmation.ascx" TagPrefix="uc" TagName="DeleteConfirmation" %>
<%@ Register Src="~/ActivityLog.ascx" TagPrefix="uc" TagName="ActivityLog" %>
<!--Developed by Chris Harris, Ilia Sorokin-->
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

        .staff-list {
            text-align: left;
            margin-top: 20px;
        }

        .captcha-container {
            display: flex;
            align-items: center;
            gap: 10px;
            margin-bottom: 15px;
        }

        .captcha-image {
            height: 50px;
            width: 150px;
            border: 1px solid #ddd;
            display: block;
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
        <uc:ActivityLog ID="activityLog" runat="server" Visible="false"/>
        <div class="container">
            <!-- Heading for the page, indicating it is for Admin to manage staff -->
            <h2>Admin - Manage Staff Members</h2>

            <!-- Label for displaying messages, such as success or error -->
            <asp:Label ID="lblMessage" runat="server" CssClass="message" Text=""></asp:Label>

            <!-- Button to trigger the modal for adding a new staff member -->
            <asp:Button ID="btnShowAddStaff" runat="server" CssClass="button" Text="Add Staff Member" OnClientClick="openAddModal(); return false;" /><br />

            <!-- Modal for adding a staff member, initially hidden -->
            <div id="addModal" class="modal">
                <div class="modal-content">
                    <!-- Close button for the modal -->
                    <span class="close" onclick="closeAddModal()">&times;</span>

                    <!-- Heading for the add staff modal -->
                    <h3>Add Staff Member</h3>
                    <!-- Textboxes for username and password input -->
                    <asp:TextBox ID="txtAddUsername" runat="server" CssClass="input-field" Placeholder="Enter Username"></asp:TextBox>
                    <asp:TextBox ID="txtAddPassword" runat="server" CssClass="input-field" TextMode="Password" Placeholder="Enter Password"></asp:TextBox>

                    <!-- CAPTCHA section to verify the user is not a bot -->
                    <div class="captcha-container">
                        <!-- Display CAPTCHA image -->
                        <asp:Image ID="imgCaptcha" runat="server" CssClass="captcha-image" />
                        <!-- Button to refresh CAPTCHA -->
                        <asp:Button ID="btnRefreshCaptcha" runat="server" CssClass="button" Text="Refresh" OnClick="btnRefreshCaptcha_Click" />
                    </div>
                    <!-- Textbox for CAPTCHA input -->
                    <asp:TextBox ID="txtCaptchaInput" runat="server" CssClass="input-field" Placeholder="Enter CAPTCHA"></asp:TextBox>

                    <!-- Button to add staff member -->
                    <asp:Button ID="btnAddStaff" runat="server" CssClass="button" Text="Add Staff" OnClick="btnAddStaff_Click" />
                </div>
            </div>

            <!-- Button to trigger the modal for deleting a staff member -->
            <asp:Button ID="btnShowDelete" runat="server" CssClass="button" Text="Delete Staff Member" OnClientClick="openDeleteModal(); return false;" />

            <!-- Modal for deleting a staff member, initially hidden -->
            <div id="deleteModal" class="modal">
                <div class="modal-content">
                    <!-- Close button for the delete staff modal -->
                    <span class="close" onclick="closeDeleteModal()">&times;</span>
                    <!-- Heading for the delete staff modal -->
                    <h3>Delete Staff Member</h3>
                    <!-- Include a custom user control for deletion confirmation -->
                    <uc:DeleteConfirmation ID="DeleteConfirmationControl" runat="server" />
                </div>
            </div>

            <!-- Navigation Buttons -->
            <!-- Button to log out -->
            <asp:Button ID="btnLogOut" runat="server" CssClass="button" Text="Log Out" OnClick="btnLogOut_Click" />
            <!-- Button to navigate to the homepage -->
            <asp:Button ID="btnHomePage" runat="server" CssClass="button" Text="Go to Home Page" OnClick="btnGoToDefault_Click" />
            <!-- Button to view all staff members -->
            <asp:Button ID="btnViewAllStaff" runat="server" CssClass="button" Text="View All Staff Members" OnClick="btnViewAllStaff_Click" />

            <!-- Section to display the list of staff members -->
            <div class="staff-list">
                <asp:Label ID="lblStaffList" runat="server" Text="" />
            </div>
        </div>
    </form>

    <script type="text/javascript">
        // JavaScript function to open the Add Staff modal
        function openAddModal() {
            document.getElementById("addModal").style.display = "block";
        }

        // JavaScript function to close the Add Staff modal
        function closeAddModal() {
            document.getElementById("addModal").style.display = "none";
        }

        // JavaScript function to open the Delete Staff modal
        function openDeleteModal() {
            document.getElementById("deleteModal").style.display = "block";
        }

        // JavaScript function to close the Delete Staff modal
        function closeDeleteModal() {
            document.getElementById("deleteModal").style.display = "none";
        }

        // Close the modals if clicked outside of the modal
        window.onclick = function (event) {
            const addModal = document.getElementById("addModal");
            const deleteModal = document.getElementById("deleteModal");
            if (event.target === addModal) {
                closeAddModal();
            }
            if (event.target === deleteModal) {
                closeDeleteModal();
            }
        };
    </script>
</body>
</html>