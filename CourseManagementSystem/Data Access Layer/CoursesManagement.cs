using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagementSystem
{
    public class CoursesManagement
    {

        private static List<Course> Courses =new List<Course>();
        public static  void AddCourse(Course course)=>Courses.Add(course);
       
        public static void RemoveCourse(int ID)=>  Courses.Remove(GetCourseBy(ID));
        
        public static List<Course> GetCourses() => Courses;
        
        public static Course GetCourseBy(int ID)
        {
            foreach (Course course in Courses) {
                if(course.ID == ID)
                {
                    return course;
                }
             }
            return null;
        }
        public static bool IsCourseExit(int ID) => GetCourseBy(ID) == null ? false : true;

    }
}
