using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagementSystem
{
    public enum EnrollmentStatus
    {
        Success,
        CourseNotRegistered,
        StudentNotFoundORCourseNotFound,
        StudentNotRegisteredInCourse,
        InvalidGrade,
        StudentRegisteredInCourse,
        CourseIsFull
    }

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

    public class EnrollmentRepository
    {
        // map for connect CourseID with StudentsID and Grade in course
        private readonly Dictionary<int, List<StudentEnrollment>> courseEnrollments = new();

        // map for connect StudentID with CoursesID and Grade in course

        private readonly Dictionary<int, List<CourseEnrollment>> studentEnrollments = new();

        private  void initializeEnrollmentIfNeeded(int courseID, int studentID)
        {
            // check is Course ID Registered or not 
            if (!courseEnrollments.ContainsKey(courseID))
            {
                // if not exist we add it to course enrollment
                courseEnrollments[courseID] = new List<StudentEnrollment>();
            }

            // check is Student ID Registered or not 
            if (!studentEnrollments.ContainsKey(studentID))
            {
                // if not exist we add it to Student enrollment
                studentEnrollments[studentID] = new List<CourseEnrollment>();
            }

        }

        public List<CourseEnrollment> GetStudentCoursesEnrollment(int studentID)
        {
            if (!studentEnrollments.ContainsKey(studentID))
                return null;

            return studentEnrollments[studentID];

        }
        public List<StudentEnrollment> GetCourseStudentsEnrollment(int courseID)
        {
            if (!courseEnrollments.ContainsKey(courseID))
                return null;

            return courseEnrollments[courseID];

        }

        public CourseEnrollment GetStudentCourseEnrollment(int studentID, int courseID)
        {
            if (!studentEnrollments.ContainsKey(studentID))
                return null;

            var studentCourses = studentEnrollments[studentID];

            foreach (var enrollment in studentCourses)
            {
                if (enrollment.CourseID == courseID)
                {
                    return enrollment;
                }
            }
            return null;
        }

        public StudentEnrollment GetCourseStudentEnrollment(int studentID, int courseID)
        {
            if (!courseEnrollments.ContainsKey(courseID))
                return null;

            var CourseStudents = courseEnrollments[courseID];

            foreach (var enrollment in CourseStudents)
            {
                if (enrollment.StudentID == studentID)
                {
                    return enrollment;
                }
            }
            return null;
        }

        public  void AssignGradeToStudent(int courseID, int studentID, decimal grade)
        {

            GetStudentCourseEnrollment(studentID, courseID).Grade = grade;

            GetCourseStudentEnrollment(studentID, courseID).Grade = grade;
        }

    }

}
