using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementSyetem
{
    public class SubjectsManagement
    {

        private static List<Subject> Subjects =new List<Subject>();
        public static  void AddSubject(Subject subject)=>Subjects.Add(subject);
       
        public static void RemoveSubject(int ID)=>  Subjects.Remove(SearchSubjectByID(ID));
        
        public static List<Subject> GetSubjects() => Subjects;
        
        public static Subject SearchSubjectByID(int ID)
        {
            foreach (Subject subject in Subjects) {
                if(subject.ID == ID)
                {
                    return subject;
                }
             }
            return null;
        }
        public static bool IsSubjectExit(int ID) => SearchSubjectByID(ID) == null ? false : true;

    }
}
