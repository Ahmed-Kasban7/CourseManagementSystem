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
        Cancel            
    }

    public class SemesterOffering
    {
        public int Id { get; private set; }
        public SemesterTemplate Template { get; private set; }
        public int Year { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public DateTime RegistrationOpenDate { get; private set; }
        public DateTime RegistrationCloseDate { get; private set; }

        public enSemesterStatus Status { get; private set; }
        public SemesterOffering(SemesterTemplate template, int year)
        {
            Template = template;
            Year = year;

            StartDate = new DateTime(year, template.StartMonthDay.Month, template.StartMonthDay.Day);
            EndDate = StartDate.AddDays(template.DurationInWeeks * 7);
            RegistrationOpenDate = StartDate.AddDays(-Template.RegistrationOpenBeforeStart);
            RegistrationCloseDate = StartDate; 

        }

        public bool IsRegistrationOpen(DateTime today)
        {
            return today >= RegistrationOpenDate && today < StartDate;
        }
        public void UpdateStatus(DateTime today)
        {
            if (Status == enSemesterStatus.Cancel)
                return; 

            if (today < RegistrationOpenDate)
                Status = enSemesterStatus.Upcoming;
            else if (today >= RegistrationOpenDate && today < StartDate)
                Status = enSemesterStatus.RegistrationOpen;
            else if (today >= StartDate && today <= EndDate)
                Status = enSemesterStatus.Running;
            else if (today > EndDate)
                Status = enSemesterStatus.Completed;
        }
        public void Cancel()
        {
            Status = enSemesterStatus.Cancel;
        }

    }

}
