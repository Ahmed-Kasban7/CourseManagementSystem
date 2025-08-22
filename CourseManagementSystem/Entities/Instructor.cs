using CourseManagementSystem.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagementSystem.Entities
{
    public enum EnTitle
    {
        Professor ,
        TA
    }
    public class Instructor:Person , IAccount
    {
        public  int InstructorID { get; private set; }
        public int? DepartmentID { get; private set; }
        public DateTime HireDate { get; private set; }
        public EnTitle Title { get; private set; }
        public string Specialization { get; private set; }
        public string UserName => base.NationalID;
        public string Password { get; set; }
        

        // When Create instructor 
        public Instructor(string name, int age, char gender, string email, string phone, string nationalID , string password, EnTitle title, string specialization, int? departmentID = null)
            : base(name, age, gender, email, phone, nationalID)
        {
            this.Password = password;
            this.DepartmentID = departmentID;
            Title = title;
            Specialization = specialization;
            HireDate = DateTime.Now;
        }


        // When Reading From Database  
        public Instructor(int instructorID, int departmentID, int personID, string name, int age, char gender,
                             string email, string phone, string nationalID, string password, EnTitle title, string specialization)
               : base(personID, name, age, gender, email, phone, nationalID)
        {
            InstructorID = instructorID;
            DepartmentID = departmentID;
            Password = password;
            Title = title;
            Specialization = specialization;
            HireDate = DateTime.Now; 
        }

    }
}
