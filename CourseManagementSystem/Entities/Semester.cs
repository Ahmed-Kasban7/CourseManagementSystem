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
 

    public class SemesterTemplate
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


        public int semesterTemplateID { get; private set; }   
        public enSemesterType SemesterType { get; private set; }      
        public StMonthDay StartMonthDay { get; private set; }   
        public int DurationInWeeks { get; private set; }

        public int RegistrationOpenBeforeStart { get; private set; }

        // When Create New Semester
        public SemesterTemplate(enSemesterType semesterType, StMonthDay startMonthDay, int durationInWeeks, int registrationOpenBeforeStart)
        {
            SemesterType = semesterType;
            StartMonthDay = startMonthDay;
            DurationInWeeks = durationInWeeks;
            RegistrationOpenBeforeStart = registrationOpenBeforeStart;
        }

        // When Retrive data from DB

        public SemesterTemplate(int semesterTemplateID, enSemesterType semesterType, StMonthDay startMonthDay, int durationInWeeks, int registrationOpenBeforeStart)
         : this (semesterType , startMonthDay, durationInWeeks, registrationOpenBeforeStart)
        {
            this.semesterTemplateID = semesterTemplateID;
        }
    }
}
