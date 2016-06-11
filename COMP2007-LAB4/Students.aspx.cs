﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

// using statements that are required to connect to EF DB
using COMP2007_LAB4.Models;
using System.Web.ModelBinding;
using System.Linq.Dynamic;
namespace COMP2007_LAB4
{
    public partial class Students : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // if loading the page for the first time, populate the student grid
            if (!IsPostBack)
            {
                Session["SortColumn"] = "StudentID";
                Session["SortDirection"] = "ASC";
                // Get the student data
                this.GetStudents();
            }
        }

        /**
         * <summary>
         * This method gets the student data from the DB
         * </summary>
         * 
         * @method GetStudents
         * @returns {void}
         */
        protected void GetStudents()
        {
            string sortString = Session["SortColumn"].ToString() + " " + Session["SortDirection"].ToString();
            // connect to EF
            using (DefaultConnection db = new DefaultConnection())
            {
                // query the Students Table using EF and LINQ
                var Students = (from allStudents in db.Students
                                select allStudents);

                // bind the result to the GridView
                StudentsGridView.DataSource = Students.AsQueryable().OrderBy(sortString).ToList();
                StudentsGridView.DataBind();
            }
        }

        protected void StudentsGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //store which row you clicked
            int selectedROw = e.RowIndex;

            //get the selected studentID using Grid's Collection
            int StudentID = Convert.ToInt32(StudentsGridView.DataKeys[selectedROw].Values["StudentID"]);

            //use EF to find the selectede from DB remove it
            using (DefaultConnection db = new DefaultConnection())
            {
                Student deleteStudent = (from studentRecords in db.Students
                                         where studentRecords.StudentID == StudentID
                                         select studentRecords).FirstOrDefault();

                //perform the removal from the DB
                db.Students.Remove(deleteStudent);
                db.SaveChanges();

                //refresh the grid
                this.GetStudents();
            }
        }

        protected void StudentsGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {


            //set tne new page number
            StudentsGridView.PageIndex = e.NewPageIndex;

            //refresh the grid
            this.GetStudents();
        }

        protected void PageSizeDropdownlist_SelectedIndexChanged(object sender, EventArgs e)
        {
            //set new page size
            StudentsGridView.PageSize = Convert.ToInt32( PageSizeDropdownlist.SelectedValue);

            //refresh the gird
            this.GetStudents();
        }

        protected void StudentsGridView_Sorting(object sender, GridViewSortEventArgs e)
        {
            //get the colunm to sort by
            Session["SortColumn"] = e.SortExpression;
          
            //refresh the grid
            this.GetStudents();
            //toggle the direction
            Session["SortDirection"] = Session["SortDirection"].ToString() == "ASC" ? "DESC" : "ASC";
        }

        protected void StudentsGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (IsPostBack)
            {
                if (e.Row.RowType == DataControlRowType.Header) {//check if the click is on header row
                    LinkButton linkbutton = new LinkButton();
                    for (int index = 0; index < StudentsGridView.Columns.Count; index++)
                    {
                        if (StudentsGridView.Columns[index].SortExpression == Session["SortColumn"].ToString())
                        {
                            if (Session["SortDirection"].ToString() == "ASC")
                            {
                                linkbutton.Text = "<i class='fa fa-caret-up fa-lg'></i>";
                            }
                            else {
                                linkbutton.Text = "<i class='fa fa-caret-down fa-lg'></i>";
                            }
                            e.Row.Cells[index].Controls.Add(linkbutton);
                        }
                    }
                }
            }
        }
    }
}