using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagementSystem.Entities
{
    abstract public class Person
    {

       protected string name;
       protected int age;
       protected readonly int personID;
       protected char gender;
       protected string email;
       protected string phone;
       protected string nationalID;
        public string Name
        {
            get => name;
            set => name = value ?? "Known";

        }
        public int Age
        {
            get => age;
            set => age = value < 18 ? 18 : value;
        }
        public int PersonID
        {
            get => personID;
        }
        public char Gender
        {
            get => gender;
            set
            {
                char V = char.ToUpper(value);
                gender = V == 'M' || V == 'F' ? V : 'N';
            }
        }
        public string Phone
        {
            get => phone;
            set => phone = string.IsNullOrWhiteSpace(value) ? "000-0000000" : value;
        }
        public string Email
        {
            get => email;
            set => email = string.IsNullOrWhiteSpace(value) ? "unknown@example.com" : value;
        }
        public string NationalID
        {
            get => nationalID;
            private set => nationalID = string.IsNullOrWhiteSpace(value) ? "N/A" : value;
        }

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
            this.personID = id;
        }
    }
}
