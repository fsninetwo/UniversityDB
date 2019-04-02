using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityGame.Classform
{
    class Classform
    {
        public int id { get; set; }
        public string name { get; set; }

        public Classform(int id, string name)
        {
            this.id = id;
            this.name = name;
        }

        public Classform(string name)
        {
            this.name = name;
        }
    }
}
