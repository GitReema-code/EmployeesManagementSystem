<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="EmployeesManagement.demo.Home" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        body {
            font-family: Arial, sans-serif;
        }

        h2 {
            color: #2C3E50;
        }

        p {
            font-size: 16px;
            color: #34495E;
        }

        .home-container {
            padding: 20px;
            background-color: #ECF0F1;
            border-radius: 10px;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
        }

        .home-header {
            text-align: center;
            margin-bottom: 20px;
        }

        .home-content {
            display: flex;
            justify-content: space-around;
            flex-wrap: wrap;
        }

        .home-section {
            background-color: #FFFFFF;
            padding: 20px;
            border-radius: 10px;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
            width: 30%;
            margin: 10px;
        }

        .home-section h3 {
            color: #3498DB;
            margin-bottom: 10px;
        }

        .home-section p {
            color: #7F8C8D;
            font-size: 14px;
        }
    </style>

    <div class="home-container">
        <div class="home-header">
            <h2>Welcome to the Employee Management System</h2>
            <p>Manage your employees, departments, and roles efficiently.</p>
        </div>
        <div class="home-content">
            <div class="home-section">
                <h3>Employees</h3>
                <p>View and manage employee information, add new employees, and update existing records.</p>
            </div>
            <div class="home-section">
                <h3>Departments</h3>
                <p>Organize your employees into departments, assign roles, and manage department-specific tasks.</p>
            </div>
            <div class="home-section">
                <h3>Roles</h3>
                <p>Define roles within your organization, assign permissions, and manage role-specific responsibilities.</p>
            </div>
        </div>
    </div>
</asp:Content>