using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagementSystem
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
            get => this.name;
            set => this.name=value??"Known";
            
        }
        public int Age { 
            get => this.age;
            set => this.age = value<18 ? 18 : value;
        }
        public int ID
        {
            get => this.id;
        }
        public char Gender { get => this.gender;
            set
            {
                char V = char.ToUpper(value);
                this.gender = ((V == 'M') || (V == 'F')) ? V : 'N';
            }
        }
        public string Phone { 
        get => this.phone;
            set => this.phone = string.IsNullOrWhiteSpace(value) ? "000-0000000" : value;
        }
        public string Email
        {
            get => this.email;
            set => this.email = string.IsNullOrWhiteSpace(value) ? "unknown@example.com" : value;
        }
        public string NationalID { 
               get =>this.nationalID;
            private set => this.nationalID = string.IsNullOrWhiteSpace(value) ? "N/A" : value;
        }
        public Student(string name , int age , char gender , string email, string phone ,string nationalID )
        {
            this.id = ++studentCounter;
            Name = name;
            Age = age;
            Gender = gender;
            Email = email;
            Phone = phone;
            NationalID = nationalID;
        }

        public void NotifyGardeAssign(int courseID, int studentID, decimal grade)
        {
            if (studentID == ID)
            {
                Console.WriteLine($"📢 إشعار لـ {Name}: تم تعيين درجة {grade} في الكورس {courseID}");
            }
        }
    }
}
