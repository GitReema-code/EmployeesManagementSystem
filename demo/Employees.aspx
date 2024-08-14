<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Employees.aspx.cs" Inherits="EmployeesManagement.demo.Employees" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2><strong>Employees</strong></h2>
    <p>Manage Your Employees here</p>

    <div style="text-align: left;">
        <asp:Label ID="lblOutput" runat="server" style="color: #3399FF"></asp:Label>
        <br />
        <table class="nav-justified" style="border-collapse: collapse; margin: 0 auto;">
            <tr>
                <td colspan="2" style="height: 20px; background-color: #f8f9fa;">
                    <!-- Label for output messages -->
                </td>
            </tr>
            <tr>
                <td class="text-right" style="width: 250px; font-weight: bold; background-color: #e9ecef; padding: 5px; border: 1px solid #dee2e6;">Employee ID</td>
                <td>
                    <asp:TextBox ID="txtEmployeeID" runat="server" CssClass="form-control"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="text-right" style="width: 250px; font-weight: bold; background-color: #e9ecef; padding: 5px; border: 1px solid #dee2e6;">First Name</td>
                <td>
                    <asp:TextBox ID="txtFirstName" runat="server" CssClass="form-control"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="text-right" style="width: 250px; font-weight: bold; background-color: #e9ecef; padding: 5px; border: 1px solid #dee2e6;">Last Name</td>
                <td>
                    <asp:TextBox ID="txtLastName" runat="server" CssClass="form-control"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="text-right" style="width: 250px; font-weight: bold; background-color: #e9ecef; padding: 5px; border: 1px solid #dee2e6;">Email</td>
                <td>
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="text-right" style="width: 250px; font-weight: bold; background-color: #e9ecef; padding: 5px; border: 1px solid #dee2e6;">Phone Number</td>
                <td>
                    <asp:TextBox ID="txtPhoneNumber" runat="server" CssClass="form-control"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="text-right" style="width: 250px; font-weight: bold; background-color: #e9ecef; padding: 5px; border: 1px solid #dee2e6;">Hire Date</td>
                <td>
                    <asp:TextBox ID="txtHireDate" runat="server" TextMode="Date" CssClass="form-control"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="text-right" style="width: 250px; font-weight: bold; background-color: #e9ecef; padding: 5px; border: 1px solid #dee2e6;">Salary</td>
                <td>
                    <asp:TextBox ID="txtSalary" runat="server" CssClass="form-control"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="text-right" style="width: 250px; font-weight: bold; background-color: #e9ecef; padding: 5px; border: 1px solid #dee2e6;">Department</td>
                <td>
                    <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="form-control"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="text-right" style="width: 250px; font-weight: bold; background-color: #e9ecef; padding: 5px; border: 1px solid #dee2e6;">Role</td>
                <td>
                    <asp:DropDownList ID="ddlRole" runat="server" CssClass="form-control"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="text-right" style="width: 250px; font-weight: bold; background-color: #e9ecef; padding: 5px; border: 1px solid #dee2e6;">&nbsp;</td>
                <td>
                    <asp:Button ID="btnViewAll" runat="server" Text="View All" OnClick="btnViewAll_Click" CssClass="btn btn-primary" />
                    <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="btnAdd_Click" CssClass="btn btn-success" />
                    <asp:Button ID="btnEdit" runat="server" Text="Edit" OnClick="btnEdit_Click" CssClass="btn btn-warning" />
                    <asp:Button ID="btnDelete" runat="server" Text="Delete" OnClick="btnDelete_Click" CssClass="btn btn-danger" OnClientClick="return confirmDelete();" />
                    <asp:Button ID="btnDetails" runat="server" Text="Employee Details" OnClick="btnDetails_Click" CssClass="btn btn-info" />
                </td>
            </tr>
        </table>
    </div>

    <div>
        <asp:GridView ID="gvEmployees" runat="server" AutoGenerateColumns="False" DataKeyNames="EmployeeID" OnSelectedIndexChanged="gvEmployees_SelectedIndexChanged" CssClass="table table-bordered table-striped" Visible="False">
            <Columns>
                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="EmployeeID">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkUpdate" runat="server"
                            CommandArgument='<%# Bind("EmployeeID") %>' OnClick="populateForm_Click"
                            Text='<%# Eval("EmployeeID") %>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="FirstName" HeaderText="First Name" />
                <asp:BoundField DataField="LastName" HeaderText="Last Name" />
                <asp:BoundField DataField="Email" HeaderText="Email" />
                <asp:BoundField DataField="PhoneNumber" HeaderText="Phone Number" />
                <asp:BoundField DataField="HireDate" HeaderText="Hire Date" DataFormatString="{0:dd/MM/yyyy}" />
                <asp:BoundField DataField="Salary" HeaderText="Salary" />
                <asp:BoundField DataField="Department" HeaderText="Department" />
                <asp:BoundField DataField="Role" HeaderText="Role" />
            </Columns>
        </asp:GridView>
    </div>
    <body>
    <!-- Page content -->

    <script type="text/javascript">
        function confirmDelete() {
            return confirm('Are you sure you want to delete this item?');
        }
    </script>
</body>

    <asp:Repeater ID="rptEmployeeDetails" runat="server">
    <HeaderTemplate>
        <div class="report-header">Employee Details</div>
    </HeaderTemplate>
    <ItemTemplate>
        <div class="employee-detail">
            <h3><%# Eval("FirstName") %> <%# Eval("LastName") %></h3>
            <p>Hire Date: <%# Eval("HireDate", "{0:MM/dd/yyyy}") %></p>
            <p>Email: <%# Eval("Email") %></p>
        </div>
    </ItemTemplate>
    <FooterTemplate>
        <div class="report-footer">End of Report</div>
    </FooterTemplate>
</asp:Repeater>


</asp:Content>
