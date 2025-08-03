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
        SubjectNotRegistered,
        StudentNotFoundORSubjectNotFound,
        StudentNotRegisteredInSubject,
        InvalidGrade ,
        StudentRegisteredInSubject,
        SubjectIsFull
    }

    public class Enrollment
    {
        // map for connect SubjectID with StudentID and Grade in subject
        private static Dictionary<int ,List<KeyValuePair<int ,  decimal?>>> enrollments = new Dictionary<int , List<KeyValuePair<int,  decimal?>>>();
        
        public static EnrollmentStatus RegisterASubjectToStudent(int subjectID , int studentID)
        {
            Subject subject = SubjectsManagement.SearchSubjectByID(subjectID);

            // check Is Student Exist or not ,Is Subject Exit or not 
            if (!StudentsManagement.IsStudentExit(studentID) || !SubjectsManagement.IsSubjectExit(subjectID))
                return EnrollmentStatus.StudentNotFoundORSubjectNotFound;

            // check Subject Is Not Full 
            if (subject.IsFull())
                return EnrollmentStatus.SubjectIsFull;

            // check is Subject ID Registered or not 
            if (!enrollments.ContainsKey(subjectID))
                // if not exist we add it to enrollment
                enrollments[subjectID] = new List<KeyValuePair<int, decimal?>>();

            // add student to subject with grade null for intial value
            enrollments[subjectID].Add(new KeyValuePair<int, decimal?>(studentID, null));

            // increase number of student registered in subject by 1 
            subject.NumOfStudRegisteredinSub++;
            return EnrollmentStatus.Success;
        }

        public static EnrollmentStatus AssignGradeToStudent(int subjectID, int studentID, decimal grade)
        {
            if (!StudentsManagement.IsStudentExit(studentID) || !SubjectsManagement.IsSubjectExit(subjectID))
                return EnrollmentStatus.StudentNotFoundORSubjectNotFound;

            if (!enrollments.ContainsKey(subjectID))
                return EnrollmentStatus.SubjectNotRegistered;

            var studentList = enrollments[subjectID];

            for (int i = 0; i < studentList.Count; i++)
            {
                //  if student ID (input) registered we update grade value 
                if (studentList[i].Key == studentID)
                {
                    // validation for grade
                    if (grade < 0 || grade > 100)
                        return EnrollmentStatus.InvalidGrade;

                    // Update the grade by replacing the KeyValuePair
                    studentList[i] = new KeyValuePair<int, decimal?>(studentID, grade);
                    return EnrollmentStatus.Success;
                }
            }
            //  if student ID (input) Not registered 

            return EnrollmentStatus.StudentNotRegisteredInSubject;
        }

        
        private static EnrollmentStatus ViewStudentGradeInSubject(int subjectID, int studentID, out decimal? grade)
        {
            grade = -1;
            if (!StudentsManagement.IsStudentExit(studentID) || !SubjectsManagement.IsSubjectExit(subjectID))
                return EnrollmentStatus.StudentNotFoundORSubjectNotFound;

            // check is Subject ID Registered or not 
            if (!enrollments.ContainsKey(subjectID))
                return EnrollmentStatus.SubjectNotRegistered;

            var studentList = enrollments[subjectID];
            foreach (var student in studentList) {

                if (student.Key == studentID)
                {
                    grade = student.Value.Value;
                    return EnrollmentStatus.Success;
                }
            }

            return EnrollmentStatus.StudentNotRegisteredInSubject;
        }
        private static EnrollmentStatus ValidateStudentAndSubject(int subjectID, int studentID)
        {
            // check Is Student Exist or not ,Is Subject Exit or not 
            if (!StudentsManagement.IsStudentExit(studentID) || !SubjectsManagement.IsSubjectExit(subjectID))
                return EnrollmentStatus.StudentNotFoundORSubjectNotFound;

            // check is Subject ID Registered or not 
            if (!enrollments.ContainsKey(subjectID))
                return EnrollmentStatus.SubjectNotRegistered;

            return EnrollmentStatus.Success;
        }


    }
}

