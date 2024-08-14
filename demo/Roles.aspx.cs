using EmployeesManagement.App_Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EmployeesManagement.demo
{
    public partial class Roles : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PanelRole1.Visible = false; // Hide the panel initially
                PanelRole2.Visible = false; // Hide the add role panel initially
                PanelRoleDetails.Visible = false; // Hide the role details panel initially
            }
        }

        protected void btnViewAllRoles_Click(object sender, EventArgs e)
        {
            PanelRole1.Visible = true; // Show the panel for viewing roles
            PanelRole2.Visible = false; // Hide the add role panel
            PanelRoleDetails.Visible = false; // Hide the details panel
            GridViewRole1.DataBind(); // Bind the data to the GridView
        }

        protected void btnAddRole_Click(object sender, EventArgs e)
        {
            PanelRole1.Visible = false; // Hide the panel for viewing roles
            PanelRole2.Visible = true; // Show the add role panel
            PanelRoleDetails.Visible = false; // Hide the details panel
        }

        protected void btnRoleDetails_Click(object sender, EventArgs e)
        {
            PanelRole1.Visible = false; // Hide the panel for viewing roles
            PanelRole2.Visible = false; // Hide the add role panel
            PanelRoleDetails.Visible = true; // Show the details panel
        }

        protected void btnGetRoleDetails_Click(object sender, EventArgs e)
        {
            int roleID;
            if (int.TryParse(txtRoleID.Text.Trim(), out roleID))
            {
                // Set the parameter for SqlDataSourceRoleDetails
                SqlDataSourceRoleDetails.SelectParameters["RoleID"].DefaultValue = roleID.ToString();

                // Bind the data to DetailsView
                DetailsViewRole.DataBind();

                // Check if any data is retrieved
                if (DetailsViewRole.DataItem == null)
                {
                    lblOutput.Text = "No role found with the given ID.";
                }
                else
                {
                    lblOutput.Text = string.Empty; // Clear previous messages
                }
            }
            else
            {
                // Handle invalid input
                lblOutput.Text = "Please enter a valid Role ID.";
            }
        }

        protected void btnSaveRole_Click(object sender, EventArgs e)
        {
            string roleName = txtRoleName.Text.Trim();
            if (!string.IsNullOrEmpty(roleName))
            {
                CRUD myCrud = new CRUD();
                string mySql = "INSERT INTO Roles (RoleName) VALUES (@RoleName)";
                Dictionary<string, object> myPara = new Dictionary<string, object>
                {
                    {"@RoleName", roleName }
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

                // Clear the text box
                txtRoleName.Text = string.Empty;

                // Switch back to the view all roles panel and update the GridView
                PanelRole1.Visible = true;
                PanelRole2.Visible = false;
                GridViewRole1.DataBind();
            }
        }

        protected void btnCloseRoleDetails_Click(object sender, EventArgs e)
        {
            // Hide the role details panel
            PanelRoleDetails.Visible = false;

            // Show the panel for viewing roles
            PanelRole1.Visible = true;
        }
    }
}