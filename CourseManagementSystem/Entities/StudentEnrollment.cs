using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagementSystem.Entities
{
    public class StudentEnrollment
    {
        public int StudentID { get; set; }
        public decimal? Grade { get; set; } // nullable
        public StudentEnrollment(int studentID, decimal? grade)
        {
            this.StudentID = studentID;
            this.Grade = grade;
        }
        public StudentEnrollment() { }
    }

}
