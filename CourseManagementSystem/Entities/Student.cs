using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagementSystem.Entities
{
    public class Student:Person ,IAccount
    {
        private readonly int studentID;
        public string UserName { get;}
        public string Password { get; set; }
        public int StudentID => studentID;
        // When Create Student
        public Student(string name, int age, char gender, string email, string phone, string nationalID , string password)
            : base(name, age, gender, email, phone, nationalID)
        {
            UserName = nationalID;
            Password = password;
        }

        // When Reading student from database
        public Student(int studentID,int PersonID,string name, int age, char gender, string email, string phone, string nationalID, string password )
            : base(PersonID, name, age, gender, email, phone, nationalID)
        {
            this.UserName = nationalID;
            this.Password = password;
            this.studentID = studentID;
        }


    }
}
