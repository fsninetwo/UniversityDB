using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityGame.Semestor_Subject
{
    class SemestorSubject
    {
        public int id { get; set; }
        public int lections { get; set; }
        public int practical { get; set; }
        public int labratory { get; set; }
        public string response { get; set; }
        public int semestor { get; set; }
        public string subject { get; set; }

        public SemestorSubject(int lections, int practical, int labratory, string response, int semestor, string subject)
        {
            this.lections = lections;
            this.practical = practical;
            this.labratory = labratory;
            this.response = response;
            this.semestor = semestor;
            this.subject = subject;
        }

        public SemestorSubject(int id, int lections, int practical, int labratory, string response, int semestor, string subject)
        {
            this.id = id;
            this.lections = lections;
            this.practical = practical;
            this.labratory = labratory;
            this.response = response;
            this.semestor = semestor;
            this.subject = subject;
        }
    }
}
