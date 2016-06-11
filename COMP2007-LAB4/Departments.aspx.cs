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
    public partial class Departments : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) {

                Session["SortColumn"] = "DepartmentID"; // default sort column
                Session["SortDirection"] = "ASC";
                //Get Departments list
                this.GetDepartment();
            }
        }
        /**
        * <summary>
        * This method gets the department data from the DB
        * </summary>
        * 
        * @method GetDepartment
        * @returns {void}
        */
        private void GetDepartment()
        {
            using (DefaultConnection db = new DefaultConnection())
            {
                string SortString = Session["SortColumn"].ToString() + " " + Session["SortDirection"].ToString();
                //write query
                var Departments = (from allDepartments in db.Departments
                                   select allDepartments).ToList();

                DepartmentsGridView.DataSource = Departments.AsQueryable().OrderBy(SortString).ToList();
                DepartmentsGridView.DataBind();

            }
        }

        protected void DepartmentsGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (IsPostBack) {
                if (e.Row.RowType == DataControlRowType.Header)//ifheader row has been clicked
                {
                    LinkButton linkbutton = new LinkButton();
                    for (int i = 0; i < DepartmentsGridView.Columns.Count - 1; i++)
                    {
                        if (DepartmentsGridView.Columns[i].SortExpression == Session["SortColumn"].ToString())
                        {
                            if (Session["SortDirection"].ToString() == "ASC")
                            {
                                linkbutton.Text = "<i class='fa fa-caret-up fa-lg'></i>";
                            }
                            else {
                                linkbutton.Text = "<i class='fa fa-caret-down fa-lg'></i>";
                            }
                            e.Row.Cells[i].Controls.Add(linkbutton);
                        }
                    }

                }
            }
        }

        protected void DepartmentsGridView_Sorting(object sender, GridViewSortEventArgs e)
        {
            //get column to sort
            Session["SortColumn"] = e.SortExpression;

            //refresh grid
            this.GetDepartment();

            //toggle the direction
            Session["SortDirection"] = Session["SortDirection"].ToString() == "ASC" ? "DESC" : "ASC";

        }
        /**
        * <summary>
        * This event handler allows pagination to occur for the Students page
        * </summary>
        * 
        * @method DepartmentsGridView_PageIndexChanging
        * @param {object} sender
        * @param {GridViewPageEventArgs} e
        * @returns {void}
        */
        protected void DepartmentsGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            DepartmentsGridView.PageIndex = e.NewPageIndex;


            this.GetDepartment();
        }
        /**
         * <summary>
         * This event handler deletes a student from the db using EF
         * </summary>
         * 
         * @method DepartmentsGridView_RowDeleting
         * @param {object} sender
         * @param {GridViewDeleteEventArgs} e
         * @returns {void}
         */
        protected void DepartmentsGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //get selected row
            int selectedRow = e.RowIndex;
            //get selected departmentID using datakey collection
            int DepartmentID = Convert.ToInt32(DepartmentsGridView.DataKeys[selectedRow].Values["DepartmentID"]);

            //connect to EF DB
            using (DefaultConnection db = new DefaultConnection())
            {
                var deleteRecord = (from deleteRow in db.Departments
                                    where deleteRow.DepartmentID == DepartmentID
                                    select deleteRow).FirstOrDefault();

                db.Departments.Remove(deleteRecord);

                db.SaveChanges();

                this.GetDepartment();
            }
        }

        protected void PageSizeDropdownlist_SelectedIndexChanged(object sender, EventArgs e)
        {
            DepartmentsGridView.PageSize = Convert.ToInt32(PageSizeDropdownlist.SelectedValue);

            this.GetDepartment();
        }
    }
}