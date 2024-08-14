<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Roles.aspx.cs" Inherits="EmployeesManagement.demo.Roles" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container">
        <h2><strong>Roles</strong></h2>
        <p>Manage Your Roles Here</p>

        <!-- Button Panel -->
        <div class="btn-group">
            <asp:Button ID="btnViewAllRoles" runat="server" Text="View All Roles" CssClass="btn btn-primary" OnClick="btnViewAllRoles_Click" />
            <asp:Button ID="btnAddRole" runat="server" Text="Add New Role" CssClass="btn btn-success" OnClick="btnAddRole_Click" />
            <asp:Button ID="btnRoleDetails" runat="server" Text="Role Details" CssClass="btn btn-info" OnClick="btnRoleDetails_Click" />
        </div>

        <!-- Panel for Displaying Roles -->
        <asp:Panel ID="PanelRole1" runat="server" Visible="false">
            <br />
            <span style="font-size: large">All Roles<br /></span>
            <br />
            <asp:GridView ID="GridViewRole1" runat="server" AutoGenerateColumns="False" DataKeyNames="RoleID" DataSourceID="SqlDataSource2">
                <Columns>
                    <asp:BoundField DataField="RoleID" HeaderText="RoleID" InsertVisible="False" ReadOnly="True" SortExpression="RoleID" />
                    <asp:BoundField DataField="RoleName" HeaderText="RoleName" SortExpression="RoleName" />
                </Columns>
            </asp:GridView>
        </asp:Panel>

        <!-- Panel for Adding New Role -->
        <asp:Panel ID="PanelRole2" runat="server" Visible="false">
            <br />
            <span style="font-size: large">Add New Role<br /></span>
            <br />
            <asp:TextBox ID="txtRoleName" runat="server" placeholder="Role Name"></asp:TextBox>
            <br />
            <asp:Button ID="btnSaveRole" runat="server" Text="Save" CssClass="btn btn-success" OnClick="btnSaveRole_Click" />
            <br />
            <asp:Label ID="lblOutput" runat="server" CssClass="text-success"></asp:Label>
        </asp:Panel>

        <!-- Panel for Role Details -->
        <asp:Panel ID="PanelRoleDetails" runat="server" Visible="false">
            <br />
            <span style="font-size: large">Role Details<br /></span>
            <br />
            <asp:TextBox ID="txtRoleID" runat="server" placeholder="Enter Role ID"></asp:TextBox>
            <asp:Button ID="btnGetRoleDetails" runat="server" Text="Get Details" CssClass="btn btn-primary" OnClick="btnGetRoleDetails_Click" />
            <br />
            <asp:DetailsView ID="DetailsViewRole" runat="server" AutoGenerateRows="False" DataSourceID="SqlDataSourceRoleDetails">
                <Fields>
                    <asp:BoundField DataField="RoleID" HeaderText="Role ID" />
                    <asp:BoundField DataField="RoleName" HeaderText="Role Name" />
                </Fields>
            </asp:DetailsView>
            <br />
            <asp:Button ID="btnCloseRoleDetails" runat="server" Text="Close" CssClass="btn btn-secondary" OnClick="btnCloseRoleDetails_Click" />
        </asp:Panel>

        <!-- SqlDataSource for Roles -->
        <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
            ConnectionString="<%$ ConnectionStrings:EmployeeManagementDB %>" 
            SelectCommand="SELECT RoleID, RoleName FROM Roles">
        </asp:SqlDataSource>
        
        <!-- SqlDataSource for Role Details -->
        <asp:SqlDataSource ID="SqlDataSourceRoleDetails" runat="server" 
            ConnectionString="<%$ ConnectionStrings:EmployeeManagementDB %>" 
            SelectCommand="SELECT RoleID, RoleName FROM Roles WHERE RoleID = @RoleID">
            <SelectParameters>
                <asp:ControlParameter Name="RoleID" ControlID="txtRoleID" PropertyName="Text" Type="Int32" />
            </SelectParameters>
        </asp:SqlDataSource>
    </div>
</asp:Content>