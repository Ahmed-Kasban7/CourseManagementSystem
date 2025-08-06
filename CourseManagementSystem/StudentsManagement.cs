using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagementSystem
{
    public class StudentsManagement
    {
        private static List<Student> Students = new List<Student>();
        public static void AddNewStudent(Student student) =>  Students.Add(student);
       
        public static List<Student> GetStudents() => Students ;

        public static void RemoveStudent(int ID) => Students.Remove(GetStudentBy(ID));
        public static Student GetStudentBy(int ID )
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

        public static bool IsStudentExit(int ID)=> GetStudentBy(ID)==null ? false : true;
    }
}
