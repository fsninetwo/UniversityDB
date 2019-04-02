using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityGame.Department
{
    class Department
    {
        public int id { get; set; }
        public string name { get; set; }
        public string faculty { get; set; }
        public string description { get; set; }

        public Department(string name, string description, string faculty)
        {
            this.name = name;
            this.faculty = faculty;
            this.description = description;
        }
        public Department(int id, string name, string description, string faculty)
        {
            this.id = id;
            this.name = name;
            this.faculty = faculty;
            this.description = description;
        }
    }
}
