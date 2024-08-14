<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Departments.aspx.cs" Inherits="EmployeesManagement.demo.Departments" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

<div class="container">
        <h2><strong>Departments</strong></h2>
        <p>Manage Your Departments Here</p>

        <!-- Button Panel -->
        <div class="btn-group">
            <asp:Button ID="btnViewAllDep" runat="server" Text="View All Departments" CssClass="btn btn-primary" OnClick="btnViewAllDep_Click" />
            <asp:Button ID="btnAddDep" runat="server" Text="Add New Department" CssClass="btn btn-success" OnClick="btnAddDep_Click"/>
            <asp:Button ID="btnDepDetails" runat="server" Text="Department Details" CssClass="btn btn-info" OnClick="btnDepDetails_Click"/>
        </div>

        <!-- Panel for Displaying Departments with Repeater -->
        <asp:Panel ID="PanelDep1" runat="server" Visible="false">
            <br />
            <span style="font-size: large">All Departments<br /></span>
            <br />
            <asp:Repeater ID="rptDepartments" runat="server">
                <ItemTemplate>
                    <div class="department-item">
                        <h4>Department ID: <%# Eval("DepartmentID") %></h4>
                        <p>Department Name: <%# Eval("DepartmentName") %></p>
                    </div>
                </ItemTemplate>
                <SeparatorTemplate>
                    <hr />
                </SeparatorTemplate>
            </asp:Repeater>
        </asp:Panel>


        <!-- Panel for Adding New Department -->
        <asp:Panel ID="PanelDep2" runat="server" Visible="false">
            <br />
            <span style="font-size: large">Add New Department<br /></span>
            <br />
            <asp:TextBox ID="txtDepartmentName" runat="server" placeholder="Department Name"></asp:TextBox>
            <br />
            <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-success" OnClick="btnSave_Click" />
            <br />
            <asp:Label ID="lblOutput" runat="server" CssClass="text-success"></asp:Label>
        </asp:Panel>

        <!-- Panel for Department Details -->
        <asp:Panel ID="PanelDepDetails" runat="server" Visible="false">
            <br />
            <span style="font-size: large">Department Details<br /></span>
            <br />
            <!-- TextBox for Department ID -->
            <asp:TextBox ID="txtDepartmentID" runat="server" placeholder="Enter Department ID"></asp:TextBox>
            <asp:Button ID="btnGetDetails" runat="server" Text="Get Details" CssClass="btn btn-primary" OnClick="btnGetDetails_Click" />
            <br />
            <asp:DetailsView ID="DetailsViewDepartment" runat="server" AutoGenerateRows="False" DataSourceID="SqlDataSourceDetails">
                <Fields>
                    <asp:BoundField DataField="DepartmentID" HeaderText="Department ID" />
                    <asp:BoundField DataField="DepartmentName" HeaderText="Department Name" />
                </Fields>
            </asp:DetailsView>
            <br />
            <asp:Button ID="btnCloseDetails" runat="server" Text="Close" CssClass="btn btn-secondary" OnClick="btnCloseDetails_Click" />
        </asp:Panel>

        <!-- SqlDataSource for Departments -->
        <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
            ConnectionString="<%$ ConnectionStrings:EmployeeManagementDB %>" 
            SelectCommand="SELECT DepartmentID, DepartmentName FROM Departments">
        </asp:SqlDataSource>

        <!-- SqlDataSource for Department Details -->
        <asp:SqlDataSource ID="SqlDataSourceDetails" runat="server"
            ConnectionString="<%$ ConnectionStrings:EmployeeManagementDB %>"
            SelectCommand="SELECT DepartmentID, DepartmentName FROM Departments WHERE DepartmentID = @DepartmentID">
            <SelectParameters>
                <asp:ControlParameter Name="DepartmentID" ControlID="txtDepartmentID" PropertyName="Text" Type="Int32" />
            </SelectParameters>
        </asp:SqlDataSource>

    </div>
</asp:Content>