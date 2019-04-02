using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityGame.Group
{
    class Group
    {

        public int id { get; set; }
        public string name { get; set; }
        public string department { get; set; }

        public Group(int id, string name, string department)
        {
            this.id = id;
            this.name = name;
            this.department = department;
        }

        public Group(string name, string department)
        {
            this.name = name;
            this.department = department;
        }
    }
}
