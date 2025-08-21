using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagementSystem.Entities
{
    public class Level
    {
        private readonly int id;
        private string name;
        private string description;
        public int ID => id;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        // When Create New Level
        public Level(string name, string description)
        {
            this.name = name;
            this.description = description;
        }

        // When retrive Levels Data from Data Base
        public Level(int id, string name, string description)
            : this(name, description)
        {
            this.id = id;
        }



    }
}
