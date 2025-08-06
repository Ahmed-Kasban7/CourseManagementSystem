using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagementSystem
{
    public class Course
    {
        private static int courseCounter = 0;
        private int id;
        private string name;
        private short hours;
        private int numOfStudRegisteredinSub;
        private string instructorName;
        private int maxLimit;

        public int MaxLimit { get => this.maxLimit; set=>this.maxLimit = value<0 ? 0 : value; }
        public int ID {  get;}

        public string Name { get => this.name;
            set => this.name = value ?? "Known";
        }
        public short Hours { get =>this.hours;
            set => this.hours = value < 0 ? (short) 0 : value;
        }
        public string InstructorName { get => this.instructorName;
            set=>this.instructorName= value ?? "Known";
        }
        public int NumOfStudRegisteredinSub { get => this.numOfStudRegisteredinSub; set => this.numOfStudRegisteredinSub = value; }
        public Course(string name , short hours , string instructorName , int maxLimit)
        {
            this.id = ++courseCounter;
            this.Name = name;
            this.Hours = hours;
            this.InstructorName = instructorName;
            this.numOfStudRegisteredinSub = 0;
            this.maxLimit = maxLimit;
        }
        public bool IsFull() => maxLimit == numOfStudRegisteredinSub;
    }

}
