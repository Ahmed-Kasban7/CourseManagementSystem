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
 

    public class Semester
    {
        public readonly struct StMonthDay {
            public int Month { get; }
            public int Day { get; }
            public StMonthDay(int month, int day)
            {
                Month = month;
                Day = day;
            }
        }


        public int SemesterID { get; private set; }   
        public enSemesterType SemesterType { get; private set; }      
        public StMonthDay StartMonth { get; private set; }   
        public int DurationInMonths { get; private set; }

        public int RegistrationOpenBeforeStart { get; private set; }
        // When Create New Semester
        public Semester(enSemesterType semesterType, StMonthDay startMonth, int durationInMonths, int registrationOpenBeforeStart)
        {
            SemesterType = semesterType;
            StartMonth = startMonth;
            DurationInMonths = durationInMonths;
            RegistrationOpenBeforeStart = registrationOpenBeforeStart;
        }

        // When Retrive data from DB

        public Semester(int semesterID,enSemesterType semesterType, StMonthDay startMonth, int durationInMonths , int registrationOpenBeforeStart)
         : this (semesterType , startMonth , durationInMonths , registrationOpenBeforeStart)
        {
            this.SemesterID = semesterID;
        }
    }
}
