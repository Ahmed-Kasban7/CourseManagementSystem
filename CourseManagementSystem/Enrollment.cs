using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
//using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using 

namespace CourseManagementSystem        
{
    public enum EnrollmentStatus
    {
        Success,
        CourseNotRegistered,
        StudentNotFoundORCourseNotFound,
        StudentNotRegisteredInCourse,
        InvalidGrade ,
        StudentRegisteredInCourse,
        CourseIsFull
    }
    public class StudentEnrollment {
       public int StudentID { get; set; }
       public decimal? Grade { get; set; } // nullable
        public StudentEnrollment(int studentID, decimal? grade)
        {
            this.StudentID = studentID;
            this.Grade = grade;
        }
        public StudentEnrollment() { }
    }

    public class CourseEnrollment {
    
        public int CourseID { get; set; }
        public decimal? Grade { get; set; } // nullable
        public CourseEnrollment(int courseID, decimal? grade)
        {
            this.CourseID = courseID;
            this.Grade = grade;
        }
        public CourseEnrollment() { }

    }

    public class StudentCourseGradeResult
    {
        public EnrollmentStatus Status { get; set; }
        public Course Course { get; set; }
        public Student Student { get; set; }
        public decimal? Grade { get; set; }
    }


    public class Enrollment
    {
        // map for connect CourseID with StudentsID and Grade in course
        private static Dictionary<int ,List<StudentEnrollment>> courseEnrollments = new Dictionary<int , List<StudentEnrollment>>();

        // map for connect StudentID with CoursesID and Grade in course

        private static Dictionary<int, List<CourseEnrollment>> studentEnrollments = new Dictionary<int, List<CourseEnrollment>>();

        private static CourseEnrollment GetStudentCourseEnrollment(int studentID , int courseID)
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
        private static StudentEnrollment GetCourseStudentEnrollment(int studentID, int courseID)
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

        public static EnrollmentStatus RegisterACourseToStudent(int courseID , int studentID)
        {

            var validationResult = validateStudentAndCourse(courseID, studentID);

            if (validationResult != EnrollmentStatus.Success)
                return validationResult;

            if (isCourseFull(courseID))
                return EnrollmentStatus.CourseIsFull;


            initializeEnrollmentIfNeeded(courseID, studentID);
            addStudentAndCourseEnrollment(courseID, studentID);
            increaseCourseStudentCount(courseID);

            return EnrollmentStatus.Success;
        }
        
        public static EnrollmentStatus AssignGradeToStudent(int courseID, int studentID, decimal grade)
        {
            var validate = validateStudentAndCourse(courseID, studentID);
            if (validate != EnrollmentStatus.Success)
                return validate;

            // validation for grade

            if (grade < 0 || grade > 100)
                return EnrollmentStatus.InvalidGrade;

            GetStudentCourseEnrollment(studentID, courseID).Grade = grade;

            GetCourseStudentEnrollment(studentID, courseID).Grade = grade;

            //  if student ID (input) Not registered 

            return EnrollmentStatus.Success;
        }

        private static StudentCourseGradeResult GetStudentCourseGradeInfo (int courseID, int studentID)
        {
            var result = new StudentCourseGradeResult();
            result.Status= validateStudentAndCourse(courseID, studentID);
            if (result.Status != EnrollmentStatus.Success)
                return result;

            var studentList = enrollments[courseID];
            foreach (var student in studentList) {

                if (student.StudentID == studentID)
                {
                    result.Student = StudentsManagement.GetStudentBy(studentID);
                    result.Course = CoursesManagement.GetCourseBy(courseID);
                    result.Status= EnrollmentStatus.Success;
                    result.Grade = student.Grade;
                    return result;
                }
            }

            result.Status = EnrollmentStatus.StudentNotRegisteredInCourse;
            return result;
        }

      
        private static EnrollmentStatus validateStudentAndCourse(int courseID, int studentID)
        {
            if (!StudentsManagement.IsStudentExit(studentID) || !CoursesManagement.IsCourseExit(courseID))
                return EnrollmentStatus.StudentNotFoundORCourseNotFound;

            return EnrollmentStatus.Success;
        }
        private static bool isCourseFull(int courseID)
        {
            Course course = CoursesManagement.GetCourseBy(courseID);
            return course.IsFull();
        }

        private static void initializeEnrollmentIfNeeded(int courseID, int studentID)
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

        private static void increaseCourseStudentCount(int courseID)
        {
            var course = CoursesManagement.GetCourseBy(courseID);
            course.NumOfStudRegisteredinSub++;

        }

        private static void addStudentAndCourseEnrollment(int courseID, int studentID)
        {
            // add student to course with grade null for intial value
            courseEnrollments[courseID].Add(new StudentEnrollment(studentID, null));
            // add course to student with grade null for intial value
            studentEnrollments[studentID].Add(new CourseEnrollment(courseID, null));
        }
    }
}

