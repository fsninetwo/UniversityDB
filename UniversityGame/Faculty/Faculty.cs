using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityGame.Faculty
{
    class Faculty
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }

        public Faculty(string name, string description)
        {
            this.name = name;
            this.description = description;
        }
        public Faculty(int id, string name, string description)
        {
            this.id = id;
            this.name = name;
            this.description = description;
        }
    }
}
