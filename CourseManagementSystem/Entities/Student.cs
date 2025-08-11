using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagementSystem.Entities
{
    public class Student
    {
        private  static int studentCounter=0;
        private string name;
        private int age;
        private readonly int id;
        private char gender;
        private string email;
        private string phone;
        private string nationalID;
        public string Name {
            get => name;
            set => name=value??"Known";
            
        }
        public int Age { 
            get => age;
            set => age = value<18 ? 18 : value;
        }
        public int ID
        {
            get => id;
        }
        public char Gender { get => gender;
            set
            {
                char V = char.ToUpper(value);
                gender = V == 'M' || V == 'F' ? V : 'N';
            }
        }
        public string Phone { 
        get => phone;
            set => phone = string.IsNullOrWhiteSpace(value) ? "000-0000000" : value;
        }
        public string Email
        {
            get => email;
            set => email = string.IsNullOrWhiteSpace(value) ? "unknown@example.com" : value;
        }
        public string NationalID { 
               get =>nationalID;
            private set => nationalID = string.IsNullOrWhiteSpace(value) ? "N/A" : value;
        }
        public Student(string name , int age , char gender , string email, string phone ,string nationalID )
        {
            id = ++studentCounter;
            Name = name;
            Age = age;
            Gender = gender;
            Email = email;
            Phone = phone;
            NationalID = nationalID;
        }

       
    }
}
