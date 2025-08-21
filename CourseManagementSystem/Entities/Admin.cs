using CourseManagementSystem.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagementSystem.Entities
{
    public class Admin : Person , IAccount
    {
        public string UserName { get;}
        public string Password { get; set; }


    }
}
