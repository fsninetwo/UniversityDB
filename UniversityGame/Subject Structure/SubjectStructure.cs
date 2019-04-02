using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityGame.Subject_Structure
{
    class SubjectStructure
    {
        public int id { get; set; }
        public int count { get; set; }
        public string classform { get; set; }
        public string subject { get; set; }
        public SubjectStructure(int count, string classform, string subject)
        {
            this.count = count;
            this.classform = classform;
            this.subject = subject;
        }
        public SubjectStructure(int id, int count, string classform, string subject)
        {
            this.id = id;
            this.count = count;
            this.classform = classform;
            this.subject = subject;
        }
    }
}
