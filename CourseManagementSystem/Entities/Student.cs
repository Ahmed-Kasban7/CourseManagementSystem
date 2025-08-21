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
        private readonly int studentID;
        private int levelID;
        public int LevelID {
            get => levelID;
            set => levelID = value;
        }
        public string UserName => nationalID;
        public string Password { get; set; }
        public int StudentID => studentID;
        // When Create Student
        public Student(string name, int age, char gender, string email, string phone, string nationalID,int levelID , string password)
            : base(name, age, gender, email, phone, nationalID)
        {
            this.levelID= levelID;
            Password = password;
        }

        // When Reading student from database
        public Student(int studentID,int PersonID,string name, int age, char gender, string email, string phone,  string nationalID, int levelID, string password )
            : base(PersonID, name, age, gender, email, phone, nationalID)
        {
            this.levelID = levelID;
            this.Password = password;
            this.studentID = studentID;
        }


    }
}
