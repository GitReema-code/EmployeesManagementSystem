using EmployeesManagement.App_Code;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Configuration;

namespace EmployeesManagement.demo
{
    public partial class Employees : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            // Populate dropdown lists only on the initial page load
            if (!Page.IsPostBack)
            {
                PopulateDdlDepartment();
                PopulateDdlRole();
                
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            // Override this method to allow GridView to render properly
        }

        protected void PopulateDdlDepartment()
        {
            CRUD myCrud = new CRUD();
            string mySql = @"SELECT DepartmentID, DepartmentName FROM Departments";
            SqlDataReader dr = myCrud.getDrPassSql(mySql);
            ddlDepartment.DataTextField = "DepartmentName";
            ddlDepartment.DataValueField = "DepartmentID";
            ddlDepartment.DataSource = dr;
            ddlDepartment.DataBind();
        }

        protected void PopulateDdlRole()
        {
            CRUD myCrud = new CRUD();
            string mySql = @"SELECT RoleID, RoleName FROM Roles";
            SqlDataReader dr = myCrud.getDrPassSql(mySql);
            ddlRole.DataTextField = "RoleName";
            ddlRole.DataValueField = "RoleID";
            ddlRole.DataSource = dr;
            ddlRole.DataBind();
        }

        protected void PopulateGridView()
        {
            try
            {
                CRUD myCrud = new CRUD();
                string mySql = @"SELECT e.EmployeeID, e.FirstName, e.LastName, e.Email, e.PhoneNumber, e.HireDate, e.Salary,
                         d.DepartmentName AS Department, r.RoleName AS Role
                         FROM Employees e
                         LEFT JOIN Departments d ON e.DepartmentID = d.DepartmentID
                         LEFT JOIN Roles r ON e.RoleID = r.RoleID";
                SqlDataReader dr = myCrud.getDrPassSql(mySql);

                if (dr.HasRows)
                {
                    gvEmployees.DataSource = dr;
                    gvEmployees.DataBind();
                    gvEmployees.Visible = true; // Ensure GridView is visible
                }
                else
                {
                    gvEmployees.DataSource = null;
                    gvEmployees.DataBind(); // Clear GridView if no rows are returned
                    gvEmployees.Visible = false; // Hide GridView if no data is available
                }
            }
            catch (Exception ex)
            {
                lblOutput.Text = "An error occurred: " + ex.Message;
            }
        }


        protected void btnViewAll_Click(object sender, EventArgs e)
        {
            PopulateGridView();
            gvEmployees.Visible = true;
            ClearForm();
        }

        private int GetNextAvailableId()
        {
            int nextId = 1;

            string query = "SELECT MIN(EmployeeID) FROM Employees WHERE EmployeeID NOT IN (SELECT EmployeeID FROM Employees)";

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["EmployeeManagementSystem"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                object result = cmd.ExecuteScalar();

                if (result != DBNull.Value && result != null)
                {
                    nextId = Convert.ToInt32(result);
                }
                else
                {
                    // If no gaps found, use the next max ID + 1
                    query = "SELECT ISNULL(MAX(EmployeeID), 0) + 1 FROM Employees";
                    cmd.CommandText = query;
                    result = cmd.ExecuteScalar();
                    nextId = Convert.ToInt32(result);
                }
            }

            return nextId;
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            gvEmployees.Visible = false;

            if (string.IsNullOrEmpty(txtFirstName.Text) || string.IsNullOrEmpty(txtLastName.Text))
            {
                lblOutput.Text = "Please provide First Name and Last Name.";
                return;
            }

            CRUD myCrud = new CRUD();
            string mySql = @"INSERT INTO Employees (FirstName, LastName, Email, PhoneNumber, HireDate, Salary, DepartmentID, RoleID)
                     VALUES (@FirstName, @LastName, @Email, @PhoneNumber, @HireDate, @Salary, @DepartmentID, @RoleID)";

            Dictionary<string, object> myPara = new Dictionary<string, object>
                    {
                        { "@FirstName", txtFirstName.Text },
                        { "@LastName", txtLastName.Text },
                        { "@Email", txtEmail.Text },
                        { "@PhoneNumber", txtPhoneNumber.Text },
                        { "@HireDate", txtHireDate.Text },
                        { "@Salary", txtSalary.Text },
                        { "@DepartmentID", ddlDepartment.SelectedValue },
                        { "@RoleID", ddlRole.SelectedValue }
                    };

            int rtn = myCrud.InsertUpdateDelete(mySql, myPara);

            lblOutput.Text = rtn >= 1 ? "Insert Operation Successful!" : "Insert Operation Failed!";
            PopulateGridView();
            ClearForm();
        }



        protected void btnEdit_Click(object sender, EventArgs e)
        {
            gvEmployees.Visible = false;
            if (string.IsNullOrEmpty(txtEmployeeID.Text))
            {
                lblOutput.Text = "You must enter a valid Employee ID.";
                return;
            }

            string mySql = @"UPDATE Employees SET FirstName = @FirstName, LastName = @LastName, Email = @Email, 
                            PhoneNumber = @PhoneNumber, HireDate = @HireDate, Salary = @Salary, 
                            DepartmentID = @DepartmentID, RoleID = @RoleID
                            WHERE EmployeeID = @EmployeeID";

            Dictionary<string, object> myPara = new Dictionary<string, object>
            {
                { "@EmployeeID", txtEmployeeID.Text },
                { "@FirstName", txtFirstName.Text },
                { "@LastName", txtLastName.Text },
                { "@Email", txtEmail.Text },
                { "@PhoneNumber", txtPhoneNumber.Text },
                { "@HireDate", txtHireDate.Text },
                { "@Salary", txtSalary.Text },
                { "@DepartmentID", ddlDepartment.SelectedValue },
                { "@RoleID", ddlRole.SelectedValue }
            };

            CRUD myCrud = new CRUD();
            int rtn = myCrud.InsertUpdateDelete(mySql, myPara);

            lblOutput.Text = rtn >= 1 ? "Update Operation Successful!" : "Update Operation Failed!";
            PopulateGridView();
            ClearForm();
        }


        protected void btnDelete_Click(object sender, EventArgs e)
        {
            gvEmployees.Visible = false;
            if (string.IsNullOrEmpty(txtEmployeeID.Text))
            {
                lblOutput.Text = "You must enter a valid Employee ID.";
                return;
            }

            CRUD myCrud = new CRUD();
            string deleteSql = @"BEGIN TRANSACTION;
                         INSERT INTO DeletedEmployeeIDs (DeletedID)
                         SELECT EmployeeID FROM Employees WHERE EmployeeID = @EmployeeID;
                         DELETE FROM Employees WHERE EmployeeID = @EmployeeID;
                         COMMIT TRANSACTION;";
            Dictionary<string, object> deletePara = new Dictionary<string, object> { { "@EmployeeID", txtEmployeeID.Text } };

            int rtn = myCrud.InsertUpdateDelete(deleteSql, deletePara);

            lblOutput.Text = rtn >= 1 ? "Delete Operation Successful!" : "Delete Operation Failed!";
            PopulateGridView();
            ClearForm();
        }


        protected void btnDetails_Click(object sender, EventArgs e)
        {
            gvEmployees.Visible = false;
            if (string.IsNullOrEmpty(txtEmployeeID.Text))
            {
                lblOutput.Text = "You must enter a valid Employee ID.";
                return;
            }

            string mySql = @"SELECT * FROM Employees WHERE EmployeeID = @EmployeeID";
            Dictionary<string, object> myPara = new Dictionary<string, object> { { "@EmployeeID", txtEmployeeID.Text } };

            CRUD myCrud = new CRUD();
            using (SqlDataReader dr = myCrud.getDrPassSql(mySql, myPara))
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        txtFirstName.Text = dr["FirstName"].ToString();
                        txtLastName.Text = dr["LastName"].ToString();
                        txtEmail.Text = dr["Email"].ToString();
                        txtPhoneNumber.Text = dr["PhoneNumber"].ToString();
                        txtHireDate.Text = dr["HireDate"].ToString();
                        txtSalary.Text = dr["Salary"].ToString();
                        ddlDepartment.SelectedValue = dr["DepartmentID"].ToString();
                        ddlRole.SelectedValue = dr["RoleID"].ToString();
                    }
                }
                else
                {
                    lblOutput.Text = "No employee found with the provided ID.";
                }
                
            }
        }

        protected void gvEmployees_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = gvEmployees.SelectedRow;
            if (row != null)
            {
                txtEmployeeID.Text = row.Cells[1].Text; 
                btnDetails_Click(sender, e); // Load details for the selected employee
            }
        }

        protected void populateForm_Click(object sender, EventArgs e)
        {
            // Retrieve the employee ID from the clicked link button
            int employeeID = int.Parse((sender as LinkButton).CommandArgument);

            // Define the SQL query to get employee details by ID, including the Salary field
            string mySql = @"SELECT EmployeeID, FirstName, LastName, DepartmentID, RoleID, Email, PhoneNumber, Salary
                     FROM Employees
                     WHERE EmployeeID = @EmployeeID";

            // Set up parameters for the query
            Dictionary<string, object> myPara = new Dictionary<string, object>
    {
        { "@EmployeeID", employeeID }
    };

            // Instantiate the CRUD class and execute the query
            CRUD myCrud = new CRUD();
            using (SqlDataReader dr = myCrud.getDrPassSql(mySql, myPara))
            {
                // Check if the data reader has rows
                if (dr.HasRows)
                {
                    // Read and populate form fields with the employee details
                    while (dr.Read())
                    {
                        txtEmployeeID.Text = dr["EmployeeID"].ToString();
                        txtFirstName.Text = dr["FirstName"].ToString();
                        txtLastName.Text = dr["LastName"].ToString();
                        ddlDepartment.SelectedValue = dr["DepartmentID"].ToString();
                        ddlRole.SelectedValue = dr["RoleID"].ToString();
                        txtEmail.Text = dr["Email"].ToString();
                        txtPhoneNumber.Text = dr["PhoneNumber"].ToString();
                        txtSalary.Text = dr["Salary"].ToString(); // Populate the Salary field
                    }
                }
            }
        }



        public DataTable GetEmployeeDetails(int employeeId)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["EmployeeManagementSystem"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Employees WHERE EmployeeID = @EmployeeID";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@EmployeeID", employeeId);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable employeeDetails = new DataTable();
                    adapter.Fill(employeeDetails);
                    return employeeDetails;
                }
            }
        }

        private void ClearForm()
        {
            txtFirstName.Text = string.Empty;
            txtLastName.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtPhoneNumber.Text = string.Empty;
            txtHireDate.Text = string.Empty;
            txtSalary.Text = string.Empty;
            ddlDepartment.SelectedIndex = 0;
            ddlRole.SelectedIndex = 0;
        }




    }
}
