using CourseManagementSystem.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagementSystem.Entities
{
    public class Instructor:Person , IAccount
    {
        private readonly int instructorID;
        private readonly int? departmentID;
        public string UserName => nationalID;
        public string Password { get; set; }
        public int InstructorID => instructorID;
        public int? DepartmentID => departmentID;



        // When Create instructor 
        public Instructor(string name, int age, char gender, string email, string phone, string nationalID , string password, int? departmentID = null)
            : base(name, age, gender, email, phone, nationalID)
        {
            this.Password = password;
            this.departmentID = departmentID;
        }

        
        // When Reading From Database  
        public Instructor(int instructorID, int departmentID, int PersonID, string name, int age, char gender, string email, string phone
            , string nationalID ,string password)
            : base(PersonID, name, age, gender, email, phone, nationalID)
        {
            this.Password = password;
            this.instructorID = instructorID;
            this.departmentID = departmentID;

        }

    }
}
