using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
//using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
namespace CourseManagementSystem        
{
    
    public class StudentCourseGradeResult
    {
        public EnrollmentStatus Status { get; set; }
        public Course Course { get; set; }
        public Student Student { get; set; }
        public decimal? Grade { get; set; }
    }


    public class Enrollment
    {
        

       

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

