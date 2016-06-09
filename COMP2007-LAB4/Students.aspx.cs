using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

// using statements that are required to connect to EF DB
using COMP2007_LAB4.Models;
using System.Web.ModelBinding;

namespace COMP2007_LAB4
{
    public partial class Students : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // if loading the page for the first time, populate the student grid
            if (!IsPostBack)
            {
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
            // connect to EF
            using (DefaultConnection db = new DefaultConnection())
            {
                // query the Students Table using EF and LINQ
                var Students = (from allStudents in db.Students
                                select allStudents);

                // bind the result to the GridView
                StudentsGridView.DataSource = Students.ToList();
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
    }
}