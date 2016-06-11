using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
// using statements required for EF DB access
using COMP2007_LAB4.Models;
using System.Web.ModelBinding;
using System.Linq.Dynamic;
namespace COMP2007_LAB4
{
    public partial class DepartmentDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && Request.QueryString.Count > 0)
            {
                GetDepartment();
            }
        }

        private void GetDepartment()
        {
            //get Department ID from query string
            int DepartmentID = Convert.ToInt32(Request.QueryString["DepartmentID"]);

            //connect to EF DB
            using (DefaultConnection db = new DefaultConnection())
            {
                //write query
                var Departnemt = (from records in db.Departments
                                  where records.DepartmentID == DepartmentID
                                  select records).FirstOrDefault();

                DepartmentTextBox.Text = Departnemt.Name;
                BudgetTextBox.Text = Departnemt.Budget.ToString();

            }
        }

        protected void SaveButton_Click(object sender, EventArgs e)
        {
            int DepartmentID = 0;
            Department departments = new Department();
            using (DefaultConnection db = new DefaultConnection())
            {
                if (Request.QueryString.Count > 0) {
                DepartmentID = Convert.ToInt32(Request.QueryString["DepartmentID"]);

               
                    departments = (from records in db.Departments
                                   where records.DepartmentID == DepartmentID
                                   select records).FirstOrDefault();
                        }
            

            departments.Name = DepartmentTextBox.Text;
            departments.Budget =Convert.ToDecimal (BudgetTextBox.Text);

            if (DepartmentID == 0) {
                    db.Departments.Add(departments);     
            }
                db.SaveChanges();
                Response.Redirect("~/Departments.aspx");
            }
        }

        protected void CancelButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Departments.aspx");
        }
    }
}