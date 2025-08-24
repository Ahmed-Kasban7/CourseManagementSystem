using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagementSystem
{
    public class Course
    {
        public int CourseID { get; private set; }
        public string CourseName { get; private set; }
        public short CreditHours { get; private set; }
        public int ?LevelID { get; private set; }
        public string? Description { get; private set; }
        public List<int>? Prerequisites { get; private set; }

        // When Retrive Course data form DB
        public Course(int courseID, string courseName, short creditHours, int? levelID,string? description = null,
            List<int>? prerequisites = null) : this (courseName,creditHours ,levelID , description , prerequisites)
        {
            CourseID = courseID;
        }
        // When Create New Course
        public Course(string courseName, short creditHours, int? levelID, string? description = null, List<int>? prerequisites = null)
        {
            CourseName = courseName;
            CreditHours = creditHours;
            LevelID = levelID;
            Description = description;
            Prerequisites = prerequisites ?? new List<int>();
        }



    }


}
