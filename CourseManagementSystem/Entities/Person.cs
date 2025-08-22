using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagementSystem.Entities
{
    abstract public class Person
    {

        public string Name { get; private set; }
        public int Age { get; private set; }
        public  int PersonID { get; private set; }
        public char Gender { get; private set; }
        public string Email { get; private set; }
        public string Phone { get; private set; }
        public string NationalID { get; private set; }


        // When Create Person 
        public Person(string name, int age, char gender, string email, string phone, string nationalID)
        {
            Name = name;
            Age = age;
            Gender = gender;
            Email = email;
            Phone = phone;
            NationalID = nationalID;
        }

        // When Read Data from Database
        public Person(int id, string name, int age, char gender, string email, string phone, string nationalID)
            : this(name, age, gender, email, phone, nationalID)
        {
            this.PersonID = id;
        }
    }
}
