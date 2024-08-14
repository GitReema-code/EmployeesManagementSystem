using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EmployeesManagement.demo
{
    public partial class Reports : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Initial setup if necessary
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            // Required for exporting to Excel
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            string query = "";

            if (rbEmployeeSummary.Checked)
            {
                query = "SELECT FirstName, LastName, HireDate FROM Employees";
                if (chkIncludeSalary.Checked)
                {
                    query += ", Salary";
                }
                if (chkIncludeContactInfo.Checked)
                {
                    query += ", Email, PhoneNumber";
                }
            }
            else if (rbRoleWise.Checked)
            {
                query = "SELECT e.FirstName, e.LastName, e.HireDate, r.RoleName FROM Employees e INNER JOIN Roles r ON e.RoleID = r.RoleID";
                if (chkIncludeSalary.Checked)
                {
                    query += ", e.Salary";
                }
                if (chkIncludeContactInfo.Checked)
                {
                    query += ", e.Email, e.PhoneNumber";
                }
            }
            else if (rbDepartmentWise.Checked)
            {
                query = "SELECT e.FirstName, e.LastName, e.HireDate, d.DepartmentName FROM Employees e INNER JOIN Departments d ON e.DepartmentID = d.DepartmentID";
                if (chkIncludeSalary.Checked)
                {
                    query += ", e.Salary";
                }
                if (chkIncludeContactInfo.Checked)
                {
                    query += ", e.Email, e.PhoneNumber";
                }
            }

            if (!string.IsNullOrEmpty(query))
            {
                DataTable dt = GetData(query);
                gvExport.DataSource = dt;
                gvExport.DataBind();
                ExportGridToExcel(gvExport);
            }
        }

        private DataTable GetData(string query)
        {
            DataTable dt = new DataTable();
            string connString = System.Configuration.ConfigurationManager.ConnectionStrings["EmployeeManagementSystem"].ConnectionString;
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

        public static void ExportGridToExcel(GridView myGv)
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.Charset = "";
            string FileName = "ExportedReport_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
            StringWriter strwritter = new StringWriter();
            HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
            HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
            myGv.GridLines = GridLines.Both;
            myGv.HeaderStyle.Font.Bold = true;

            // Ensure the GridView is visible when exporting
            myGv.Visible = true;
            myGv.RenderControl(htmltextwrtter);

            HttpContext.Current.Response.Write(strwritter.ToString());
            HttpContext.Current.Response.Flush(); // Flush the data to the output stream
            HttpContext.Current.Response.End();
        }
    }
}
