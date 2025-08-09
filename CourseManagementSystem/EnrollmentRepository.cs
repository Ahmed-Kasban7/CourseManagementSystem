using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
            addCourse(courseID);

            addstudent(studentID);
        }

        private void addCourse(int courseID)
        {
            if(!courseEnrollments.ContainsKey(courseID))
            courseEnrollments[courseID]=new List<StudentEnrollment>();
        }
        private void addstudent(int studentID)
        {
            if(!studentEnrollments.ContainsKey(studentID))
            studentEnrollments[studentID] = new List<CourseEnrollment>();
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

        public delegate void GradeAssignedToStudentEventHandler(int courseID, int studentID, decimal grade);

        public event GradeAssignedToStudentEventHandler GradeAssignedToStudent;
        public  void AssignGradeToStudent(int courseID, int studentID, decimal grade)
        {
            var studentEnrollment = GetStudentCourseEnrollment(studentID, courseID);
            var courseEnrollment = GetCourseStudentEnrollment(studentID, courseID);

            if (studentEnrollment != null && courseEnrollment != null)
            {
                studentEnrollment.Grade = grade;
                courseEnrollment.Grade = grade;

                GradeAssignedToStudent?.Invoke(courseID, studentID , grade);
            }

        }

    }

}
