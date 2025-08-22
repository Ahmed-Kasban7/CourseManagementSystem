using CourseManagementSystem.Interfaces;
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
        public  int StudentID { get; private set; }
        public int LevelID {  get; private set; }
        
        public string UserName => base.NationalID;
        public string Password { get; set; }

        // When Create Student
        public Student(string name, int age, char gender, string email, string phone, string nationalID,int levelID , string password)
            : base(name, age, gender, email, phone, nationalID)
        {
            this.LevelID = levelID;
            Password = password;
        }

        // When Reading student from database
        public Student(int studentID,int PersonID,string name, int age, char gender, string email, string phone,  string nationalID, int levelID, string password )
            : base(PersonID, name, age, gender, email, phone, nationalID)
        {
            this.LevelID = levelID;
            this.Password = password;
            this.StudentID = studentID;
        }


    }
}
