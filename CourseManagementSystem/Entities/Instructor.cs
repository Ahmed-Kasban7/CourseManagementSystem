using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagementSystem.Entities
{
    public class Instructor:Person
    {
        private readonly int? instructorID;
        private readonly int?  departmentID;

        public int? InstructorID => instructorID;
        public int? DepartmentID => departmentID;



        // When Create instructor without Department
        public Instructor(string name, int age, char gender, string email, string phone, string nationalID)
            : base(name, age, gender, email, phone, nationalID)
        {
            departmentID = null;
        }

        // When Create instructor with Department
        public Instructor(string name, int age, char gender, string email, string phone, string nationalID , int department_ID)
            : base(name, age, gender, email, phone, nationalID)
        {
            this.departmentID = department_ID;
        }

        // When Reading From Database  
        public Instructor(int instructorID, int departmentID, int Person_ID, string name, int age, char gender, string email, string phone, string nationalID)
            : base(Person_ID, name, age, gender, email, phone, nationalID)
        {
            this.instructorID = instructorID;
            this.departmentID = departmentID;

        }

    }
}
