using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityGame.Schedule
{
    class Schedule
    {
        public int id { get; set; }
        public int day { get; set; }
        public TimeSpan start { get; set; }
        public TimeSpan finish { get; set; }
        public string cabinet { get; set; }
        public string group { get; set; }
        public string subject { get; set; }
        public Schedule(int day, TimeSpan start, TimeSpan finish, string cabinet, string group, string subject)
        {
            this.day = day;
            this.start = start;
            this.finish = finish;
            this.cabinet = cabinet;
            this.group = group;
            this.subject = subject;
        }
        public Schedule(int id, int day, TimeSpan start, TimeSpan finish, string cabinet, string group, string subject)
        {
            this.id = id;
            this.day = day;
            this.start = start;
            this.finish = finish;
            this.cabinet = cabinet;
            this.group = group;
            this.subject = subject;
        }
    }
}
