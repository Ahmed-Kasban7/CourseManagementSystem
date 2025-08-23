using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagementSystem.Entities
{
    public enum enSemesterType {
        Fall,
        Spring ,
        Summer

    }
    public enum enSemesterStatus {
        Upcoming,
        RegistrationOpen ,
        Running ,
        Completed ,
        Cancelled
    }

    public class Semester
    {

        public int SemesterId { get; set; }   
        public enSemesterType SemesterType { get; set; }      
        public int StartMonth { get; set; }   
        public int DurationInMonths { get; set; }

    }
}
