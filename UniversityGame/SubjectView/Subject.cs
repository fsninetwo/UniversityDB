using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityGame.SubjectView
{
    class Subject
    {
        private int id;
        private string name;
        private int lections;
        private int practical;
        private int labratory;
        private string semestors;
        public Subject()
        {

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

        public void getId(int id)
        {
            this.id = id;
        }

        public void getName(string name)
        {
            this.name = name;
        }

        public void getLections(int lections)
        {
            this.lections = lections;
        }

        public void getPractical(int practical)
        {
            this.practical = practical;
        }

        public void getLabratory(int labratory)
        {
            this.labratory = labratory;
        }

        public void getId(string semestors)
        {
            this.semestors = semestors;
        }
    }
}
