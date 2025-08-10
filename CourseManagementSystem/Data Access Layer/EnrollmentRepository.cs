using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagementSystem
{
  
    public class EnrollmentRepository
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
        public List<CourseEnrollment> GetEnrollmentsByStudent(int studentID)
        {
            if (!studentEnrollments.ContainsKey(studentID))
                return null;

            return studentEnrollments[studentID];

        }
        public List<StudentEnrollment> GetEnrollmentsByCourse(int courseID)
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
        public  void RegisterACourseToStudent(int courseID, int studentID)
        {

            initializeEnrollmentIfNeeded(courseID, studentID);
            addStudentAndCourseEnrollment(courseID, studentID);
            increaseCourseStudentCount(courseID);

        }
        private  void increaseCourseStudentCount(int courseID)
        {
            var course = CoursesManagement.GetCourseBy(courseID);
            course.NumOfStudRegisteredinSub++;

        }
        private void decreaseCourseStudentCount(int courseID)
        {
            var course = CoursesManagement.GetCourseBy(courseID);
            course.NumOfStudRegisteredinSub--;

        }

        private void addStudentAndCourseEnrollment(int courseID, int studentID)
        {
            // add student to course with grade null for intial value
            courseEnrollments[courseID].Add(new StudentEnrollment(studentID, null));
            // add course to student with grade null for intial value
            studentEnrollments[studentID].Add(new CourseEnrollment(courseID, null));
        }

        private bool isStudentEnrollmentInCourse(int courseID, int studentID)
        {
            var studentEnrollment = GetStudentCourseEnrollment(studentID, courseID);
            var courseEnrollment = GetCourseStudentEnrollment(studentID, courseID);
            return studentEnrollment != null && courseEnrollment != null;
        }
        public bool DeleteEnrollment(int courseID, int studentID)
        {
            var studentEnrollment = GetStudentCourseEnrollment(studentID, courseID);
            var courseEnrollment = GetCourseStudentEnrollment(studentID, courseID);
            if (studentEnrollment != null && courseEnrollment != null) {
                studentEnrollments[studentID].Remove(studentEnrollment);
                courseEnrollments[courseID].Remove(courseEnrollment);
                decreaseCourseStudentCount(courseID);
                return true;
            }
            return false;
        }
    }

}
