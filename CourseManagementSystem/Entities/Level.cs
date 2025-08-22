using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagementSystem.Entities
{
    public class Level
    {
       public  int ID {  get; private set; }
       public string Name {  get; private set; }
       public string Description {  get; private  set; }


        // When Create New Level
        public Level(string name, string description)
        {
            this.Name = name;
            this.Description = description;
        }

        // When retrive Levels Data from Data Base
        public Level(int id, string name, string description)
            : this(name, description)
        {
            this.ID = id;
        }



    }
}
