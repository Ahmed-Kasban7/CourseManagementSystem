using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagementSystem.Entities
{
    public enum enSemesterStatus
    {
        Upcoming,
        RegistrationOpen,
        Running,
        Completed,
        Cancelled
    }
    public class EnrollmentPeriod

    {

        public int EnrollmentPeriodId { get; private set; }
        public int SemesterId { get; private set; }
        public int Year { get; private set; }
        public DateOnly ClassesStartDate { get; private set; }
        public DateOnly ClassesEndDate { get; private set; }

        public DateOnly RegistrationOpenDate { get; private set; }
        public DateOnly RegistrationCloseDate { get; private set; }

        public enSemesterStatus Status { get; private set; }


        // When Create New Semester 
        public EnrollmentPeriod(enSemesterType type, int year, DateOnly startDate, DateOnly endDate,
            DateOnly registrationOpenDate, DateOnly registrationCloseDate)
        {
            Year = year;
            ClassesStartDate = startDate;
            ClassesEndDate = endDate;
            RegistrationOpenDate = registrationOpenDate;
            RegistrationCloseDate = registrationCloseDate;

            this.Status = DateTime.Today switch
            {
                var d when d < RegistrationOpenDate.ToDateTime(TimeOnly.MinValue)
                    => enSemesterStatus.Upcoming,

                var d when d >= RegistrationOpenDate.ToDateTime(TimeOnly.MinValue) &&
                           d <= RegistrationCloseDate.ToDateTime(TimeOnly.MaxValue)
                    => enSemesterStatus.RegistrationOpen,

                var d when d > RegistrationCloseDate.ToDateTime(TimeOnly.MaxValue) &&
                           d <= ClassesEndDate.ToDateTime(TimeOnly.MaxValue)
                    => enSemesterStatus.Running,

                var d when d > ClassesEndDate.ToDateTime(TimeOnly.MaxValue)
                    => enSemesterStatus.Completed,

                _ => enSemesterStatus.Cancelled
            };

        }

        // when retrive semster data from DB
        public EnrollmentPeriod(int semesterID, enSemesterType type, int year, DateOnly startDate, DateOnly endDate,
            DateOnly registrationOpenDate, DateOnly registrationCloseDate) : this(type, year, startDate, endDate, registrationOpenDate, registrationCloseDate)
        {
            this.SemesterId = semesterID;

        }

        public bool IsRegistrationOpen => Status == enSemesterStatus.RegistrationOpen;
        public void CancelSemester()
        {
            Status = enSemesterStatus.Cancelled;
        }


    }
}
