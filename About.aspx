<%@ Page Title="About" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="EmployeesManagement.About" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .about-container {
            padding: 20px;
            background-color: #F7F7F7;
            border-radius: 10px;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
            font-family: Arial, sans-serif;
            max-width: 800px;
            margin: auto;
        }

    </style>

    <div class="about-container">
        <div class="about-header">
            <h2 class="text-center"><strong>About</strong></h2>
        </div>
        <div class="about-description">
            <p class="text-center">&nbsp;A comprehensive system to manage employee records, including personal details, job details, and salary information. This system will streamline the process of managing employees and improve organizational efficiency.</p>
            <p class="text-center">This Employee Management System is designed to simplify the HR processes and provide a robust solution for managing employee data effectively.</p>
        </div>
        <div class="about-footer">
            <p class="text-center"><strong>Created By</strong>: Reema Alghamdi - 6253</p>
            <p class="text-center"><strong>For Contact</strong>: rimalghamdi76@gmail.com</p>
        </div>
    </div>
</asp:Content>
