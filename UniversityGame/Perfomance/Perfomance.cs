using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityGame.Perfomance
{
    class Perfomance
    {
        public int id { get; set; }
        public int mark { get; set; }
        public string character { get; set; }
        public string subject { get; set; }
        public Perfomance(int mark, string character, string subject)
        {
            this.mark = mark;
            this.character = character;
            this.subject = subject;
        }
        public Perfomance(int id, int mark, string character, string subject)
        {
            this.id = id;
            this.mark = mark;
            this.character = character;
            this.subject = subject;
        }
    }
}
