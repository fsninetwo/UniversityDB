using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityGame.Stress
{
    class Stress
    {
        public int id { get; set;  }
        public DateTime day { get; set;  }
        public int stress { get; set; }
        public string nickname { get; set; }
        public Stress(DateTime day, int stress, string nickname)
        {
            this.day = day;
            this.stress = stress;
            this.nickname = nickname;
        }
        public Stress(int id, DateTime day, int stress, string nickname)
        {
            this.id = id;
            this.day = day;
            this.stress = stress;
            this.nickname = nickname;
        }
    }
}
