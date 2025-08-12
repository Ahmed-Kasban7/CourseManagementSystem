using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagementSystem.Entities
{
    public class Student:Person
    {
        private readonly int? student_ID;

        public int? Student_ID => student_ID;
        // When Create Student
        public Student(string name, int age, char gender, string email, string phone, string nationalID)
            : base(name, age, gender, email, phone, nationalID)
        {
            
        }

        // When Reading student from database
        public Student(int student_ID,int Person_ID,string name, int age, char gender, string email, string phone, string nationalID)
            : base(Person_ID, name, age, gender, email, phone, nationalID)
        {
            this.student_ID = student_ID;
        }
    }
}
