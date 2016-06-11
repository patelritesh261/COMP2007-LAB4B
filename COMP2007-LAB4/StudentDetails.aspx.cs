using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

// using statements required for EF DB access
using COMP2007_LAB4.Models;
using System.Web.ModelBinding;

namespace COMP2007_LAB4
{
    public partial class StudentDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((!IsPostBack) && (Request.QueryString.Count > 0))
            {
                this.GetStudent();
            }
        }

        private void GetStudent()
        {
            //populate the form with existing student from the db
            int StudentID = Convert.ToInt32(Request.QueryString["StudentID"]);

            //connect to DB
            using(DefaultConnection db=new DefaultConnection())
            {
                //get specific data from DB
                var student = (from studentRecord in db.Students
                               where studentRecord.StudentID == StudentID
                               select studentRecord).FirstOrDefault();

                LastNameTextBox.Text = student.LastName;
                FirstNameTextBox.Text = student.FirstMidName;
                EnrollmentDateTextBox.Text = student.EnrollmentDate.ToString("yyyy-MM-dd");

            }
           
        }

        protected void CancelButton_Click(object sender, EventArgs e)
        {
            // Redirect back to Students page
            Response.Redirect("~/Students.aspx");
        }

        protected void SaveButton_Click(object sender, EventArgs e)
        {
            // Use EF to connect to the server
            using (DefaultConnection db = new DefaultConnection())
            {
                // use the Student model to create a new student object and
                // save a new record
                Student newStudent = new Student();

                //get id from query stirng
                int StudentID = 0;

                if (Request.QueryString.Count > 0) {
                    StudentID = Convert.ToInt32(Request.QueryString["StudentID"]);

                    newStudent = (from student in db.Students
                                  where student.StudentID == StudentID
                                  select student).FirstOrDefault();
                }

                // add form data to the new student record
                newStudent.LastName = LastNameTextBox.Text;
                newStudent.FirstMidName = FirstNameTextBox.Text;
                newStudent.EnrollmentDate = Convert.ToDateTime(EnrollmentDateTextBox.Text);

                // use LINQ to ADO.NET to add / insert new student into the database
                if(StudentID==0)
                db.Students.Add(newStudent);

                // save our changes
                db.SaveChanges();

                // Redirect back to the updated students page
                Response.Redirect("~/Students.aspx");
            }
        }
    }
}