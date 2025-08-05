using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
//using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementSyetem
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
            Course course = CoursesManagement.GetCourseBy(courseID);

            // check Is Student Exist or not ,Is Course Exit or not 
            if (!StudentsManagement.IsStudentExit(studentID) || !CoursesManagement.IsCourseExit(courseID))
                return EnrollmentStatus.StudentNotFoundORCourseNotFound;

            // check Course Is Not Full 
            if (course.IsFull())
                return EnrollmentStatus.CourseIsFull;

            // check is Course ID Registered or not 
            if (!courseEnrollments.ContainsKey(courseID))
            {
                // if not exist we add it to enrollment
                courseEnrollments[courseID] = new List<StudentEnrollment>();
                studentEnrollments[studentID] = new List<CourseEnrollment>();
            }

            // add student to course with grade null for intial value
            courseEnrollments[courseID].Add(new StudentEnrollment(studentID, null));
            // add course to student with grade null for intial value
            studentEnrollments[studentID].Add(new CourseEnrollment(courseID, null));

            // increase number of student registered in course by 1 
            course.NumOfStudRegisteredinSub++;
            return EnrollmentStatus.Success;
        }

        public static EnrollmentStatus AssignGradeToStudent(int courseID, int studentID, decimal grade)
        {
            var validate = ValidateStudentAndCourse(courseID, studentID);
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
            result.Status= ValidateStudentAndCourse(courseID, studentID);
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

      
        private static EnrollmentStatus ValidateStudentAndCourse(int courseID, int studentID)
        {
            // check Is Student Exist or not ,Is Course Exit or not 
            if (!StudentsManagement.IsStudentExit(studentID) || !CoursesManagement.IsCourseExit(courseID))
                return EnrollmentStatus.StudentNotFoundORCourseNotFound;

            // check is Course ID Registered or not 
            if (!enrollments.ContainsKey(courseID))
                return EnrollmentStatus.CourseNotRegistered;

            return EnrollmentStatus.Success;
        }


    }
}

