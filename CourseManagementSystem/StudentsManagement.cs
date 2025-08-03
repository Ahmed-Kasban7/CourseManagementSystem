using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementSyetem
{
    public class StudentsManagement
    {
        private static List<Student> Students = new List<Student>();
        public static void AddNewStudent(Student student) =>  Students.Add(student);
       
        public static List<Student> GetStudents() => Students ;

        public static void RemoveStudent(int ID) => Students.Remove(SearchStudentByID(ID));
        public static Student SearchStudentByID(int ID )
        {
            foreach (Student s in Students)
            {
                if (s.ID == ID)
                {
                    return s;
                }
            }
            return null;
        }

        public static bool IsStudentExit(int ID)=>SearchStudentByID(ID)==null ? false : true;
    }
}
