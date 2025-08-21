using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagementSystem
{
    public class Course
    {
        private readonly int courseID;
        private string courseName;
        private short creditHours;
        private int levelID;
        private List<int> Prerequisites;

        public int LevelID
        {
            get => levelID;
            set => levelID = value;
        }
        public int ID {  get;}

        public string CourseName
        { 
            get => this.courseName;
            set => this.courseName = value ?? "UnKnown";
        }
        public short CreditHours
        { 
            get =>this.creditHours;
            set => this.creditHours = value < 0 ? (short) 0 : value;
        }

        public Course(string name , short hours , string instructorName , int maxLimit)
        {
            this.courseName = name;
            this.creditHours = hours;
        }
    }

}
