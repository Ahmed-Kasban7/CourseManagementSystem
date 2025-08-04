using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
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
    public class EnrollmentInfo {
       public int StudentID { get; set; }
       public decimal? Grade { get; set; } // nullable
        public EnrollmentInfo(int studentID, decimal? grade)
        {
            this.StudentID = studentID;
            this.Grade = grade;
        }
        public EnrollmentInfo() { }
    }
    public class StudentCourseGradeResult
    {
        public EnrollmentStatus Status { get; set; }
        public int CourseID { get; set; }
        public int StudentID { get; set; }
        public decimal? Grade { get; set; }
    }


    public class Enrollment
    {
        // map for connect CourseID with StudentID and Grade in course
        private static Dictionary<int ,List<EnrollmentInfo>> enrollments = new Dictionary<int , List<EnrollmentInfo>>();
        
        public static EnrollmentStatus RegisterACourseToStudent(int courseID , int studentID)
        {
            Course course = CoursesManagement.SearchCourseByID(courseID);

            // check Is Student Exist or not ,Is Course Exit or not 
            if (!StudentsManagement.IsStudentExit(studentID) || !CoursesManagement.IsCourseExit(courseID))
                return EnrollmentStatus.StudentNotFoundORCourseNotFound;

            // check Course Is Not Full 
            if (course.IsFull())
                return EnrollmentStatus.CourseIsFull;

            // check is Course ID Registered or not 
            if (!enrollments.ContainsKey(courseID))
                // if not exist we add it to enrollment
                enrollments[courseID] = new List<EnrollmentInfo>();

            // add student to course with grade null for intial value
            enrollments[courseID].Add(new EnrollmentInfo(studentID, null));

            // increase number of student registered in course by 1 
            course.NumOfStudRegisteredinSub++;
            return EnrollmentStatus.Success;
        }

        public static EnrollmentStatus AssignGradeToStudent(int courseID, int studentID, decimal grade)
        {
            var validate = ValidateStudentAndCourse(courseID, studentID);
            if (validate != EnrollmentStatus.Success)
                return validate;

            var studentList = enrollments[courseID];

            for (int i = 0; i < studentList.Count; i++)
            {
                //  if student ID (input) registered we update grade value 
                if (studentList[i].StudentID == studentID)
                {
                    // validation for grade
                    if (grade < 0 || grade > 100)
                        return EnrollmentStatus.InvalidGrade;

                    // Update the grade by replacing the KeyValuePair
                    studentList[i].Grade= grade;
                    return EnrollmentStatus.Success;
                }
            }
            //  if student ID (input) Not registered 

            return EnrollmentStatus.StudentNotRegisteredInCourse;
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
                    result.StudentID = studentID;
                    result.CourseID = courseID;
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

