using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityGame.Skill
{
    class Skill
    {
        public int id { get; set; }
        public int condition { get; set;  }
        public string character { get; set; }
        public string subject { get; set; }
        public Skill(int condition, string character, string subject)
        {
            this.condition = condition;
            this.character = character;
            this.subject = subject;
        }
        public Skill(int id, int condition, string character, string subject)
        {
            this.id = id;
            this.condition = condition;
            this.character = character;
            this.subject = subject;
        }
    }
}
