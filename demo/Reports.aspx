<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Reports.aspx.cs" Inherits="EmployeesManagement.demo.Reports" EnableEventValidation="false" ValidateRequest="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container">
        <h2><strong>Reports</strong></h2>
        <p>Generate and Download Employees Reports Here</p>
        <br />

        <!-- Section for Report Options -->
        <div class="form-section">
            <h3>Report Options</h3>
            <p>Select the options you would like to include in the report:</p>
            <asp:CheckBox ID="chkIncludeSalary" runat="server" Text="Include Salary" />
            <asp:CheckBox ID="chkIncludeContactInfo" runat="server" Text="Include Contact Info" />
        </div>

        <!-- Radio Buttons for Report Type -->
        <div class="form-section">
            <h3>Report Type</h3>
            <asp:RadioButton ID="rbEmployeeSummary" runat="server" GroupName="ReportType" Text="Employee Summary" Checked="True" />
            <asp:RadioButton ID="rbRoleWise" runat="server" GroupName="ReportType" Text="Role-wise Report" />
            <asp:RadioButton ID="rbDepartmentWise" runat="server" GroupName="ReportType" Text="Department-wise Report" />
        </div>

        <!-- Export Button -->
        <div class="export-button-container">
            <asp:Button ID="btnExport" runat="server" Text="Export to Excel" CssClass="btn btn-success" OnClick="btnExport_Click" />
        </div>

        <!-- Hidden GridView for Export -->
        <asp:GridView ID="gvExport" runat="server" AutoGenerateColumns="True" Visible="false"></asp:GridView>
    </div>

</asp:Content>
