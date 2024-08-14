using EmployeesManagement.App_Code;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EmployeesManagement.demo
{
    public partial class Departments : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PanelDep1.Visible = false; // Hide the panel initially
                PanelDep2.Visible = false; // Hide the add department panel initially
                PanelDepDetails.Visible = false; // Hide the department details panel initially
            }
        }

        protected void btnViewAllDep_Click(object sender, EventArgs e)
        {
            PanelDep1.Visible = true; // Show the panel for viewing departments
            PanelDep2.Visible = false; // Hide the add department panel
            PanelDepDetails.Visible = false; // Hide the details panel

            // Bind data to the Repeater
            BindDepartments();
        }

        protected void btnAddDep_Click(object sender, EventArgs e)
        {
            PanelDep1.Visible = false; // Hide the panel for viewing departments
            PanelDep2.Visible = true; // Show the add department panel
            PanelDepDetails.Visible = false; // Hide the details panel
        }

        protected void btnDepDetails_Click(object sender, EventArgs e)
        {
            PanelDep1.Visible = false; // Hide the panel for viewing departments
            PanelDep2.Visible = false; // Hide the add department panel
            PanelDepDetails.Visible = true; // Show the details panel
        }

        protected void btnGetDetails_Click(object sender, EventArgs e)
        {
            int departmentID;
            if (int.TryParse(txtDepartmentID.Text.Trim(), out departmentID))
            {
                // Set the parameter for SqlDataSourceDetails
                SqlDataSourceDetails.SelectParameters["DepartmentID"].DefaultValue = departmentID.ToString();

                // Bind the data to DetailsView
                DetailsViewDepartment.DataBind();

                // Check if any data is retrieved
                if (DetailsViewDepartment.DataItem == null)
                {
                    lblOutput.Text = "No department found with the given ID.";
                }
                else
                {
                    lblOutput.Text = string.Empty; // Clear previous messages
                }
            }
            else
            {
                // Handle invalid input
                lblOutput.Text = "Please enter a valid Department ID.";
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string departmentName = txtDepartmentName.Text.Trim();
            if (!string.IsNullOrEmpty(departmentName))
            {
                CRUD myCrud = new CRUD();
                string mySql = "INSERT INTO Departments (DepartmentName) VALUES (@DepartmentName)";
                Dictionary<string, object> myPara = new Dictionary<string, object>
                {
                    {"@DepartmentName", departmentName }
                };
                int rtn = myCrud.InsertUpdateDelete(mySql, myPara);
                if (rtn >= 1)
                {
                    lblOutput.Text = "Insert Operation Successful!";
                }
                else
                {
                    lblOutput.Text = "Insert Operation Failed!";
                }

                // To clear the text box
                txtDepartmentName.Text = string.Empty;

                // Switch back to the view all departments panel and update the Repeater
                PanelDep1.Visible = true;
                PanelDep2.Visible = false;
                BindDepartments();
            }
        }

        protected void btnCloseDetails_Click(object sender, EventArgs e)
        {
            // Hide the department details panel
            PanelDepDetails.Visible = false;

            // Show the panel for viewing departments
            PanelDep1.Visible = true;
        }

        private void BindDepartments()
        {
            string query = "SELECT DepartmentID, DepartmentName FROM Departments";
            DataTable dt = GetData(query);
            rptDepartments.DataSource = dt;
            rptDepartments.DataBind();
        }

        private DataTable GetData(string query)
        {
            DataTable dt = new DataTable();
            string connString = System.Configuration.ConfigurationManager.ConnectionStrings["EmployeeManagementDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            return dt;
        }
    }
}
