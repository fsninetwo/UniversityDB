using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityGame.Subject
{
    class Subject
    {
        public int id { get; set; }
        public string name { get; set; }
        public int lections { get; set; }
        public int practical { get; set; }
        public int labratory { get; set; }
        public string semestors { get; set; }
        public Subject(string name, int lections, int practical, int labratory, string semestors)
        {
            this.name = name;
            this.lections = lections;
            this.practical = practical;
            this.labratory = labratory;
            this.semestors = semestors;
        }
        public Subject(int id, string name, int lections, int practical, int labratory, string semestors)
        {
            this.id = id;
            this.name = name;
            this.lections = lections;
            this.practical = practical;
            this.labratory = labratory;
            this.semestors = semestors;
        }
    }
}
