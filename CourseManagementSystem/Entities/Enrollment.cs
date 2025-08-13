using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagementSystem.Entities
{
    public enum EnStatus
    {
        ACTIVE,
        COMPLETED,
        DROPED,
        PENDING
    }
    public enum EnGrade
    {
        A_PLUS,
        A,
        A_MINUS,
        B_PLUS,
        B,
        B_MINUS,
        C_PLUS,
        C,
        C_MINUS,
        D_PLUS,
        D,
        D_MINUS,
        F,
        NOT_GRADED
    }
    public class Enrollment
    {
        
        private int studentID; 
        private int courseID;
        private int instructorID;
        private EnGrade grade;
        private DateOnly enrollmentDate;
        private EnStatus status;

        // When retrive Data from Database 
        public Enrollment(int studentID, int courseID, int instructorID, EnGrade grade , DateOnly enrollmentDate, EnStatus status)
        {
            this.studentID = studentID;
            this.courseID = courseID;
            this.instructorID = instructorID;
            this.grade = grade;
            this.enrollmentDate = enrollmentDate;
            this.status = status;
        }

        public Enrollment(int studentID, int courseID)
        {
            this.studentID = studentID;
            this.courseID = courseID;
            this.instructorID = instructorID;
            this.grade = grade;
            this.enrollmentDate = enrollmentDate;
            this.status = status;
        }
    }

}
