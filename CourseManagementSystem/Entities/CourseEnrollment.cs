using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagementSystem.Entities
{
    public class CourseEnrollment
    {

        public int CourseID { get; set; }
        public decimal? Grade { get; set; } // nullable
        public CourseEnrollment(int courseID, decimal? grade)
        {
            this.CourseID = courseID;
            this.Grade = grade;
        }
        public CourseEnrollment() { }

    }
}
