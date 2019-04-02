using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityGame.Character
{
    class Character
    {
        public int id { get; set; }
        public string nickname { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public DateTime registration { get; set; }
        public bool admin { get; set; }
        public string group { get; set; }

        public Character(int id, string nickname, string password, string email, DateTime registration, bool admin, string group)
        {
            this.id = id;
            this.nickname = nickname;
            this.password = password;
            this.email = email;
            this.registration = registration;
            this.admin = admin;
            this.group = group;
        }
        public Character(int id, string nickname, string password, string email, bool admin, string group)
        {
            this.id = id;
            this.nickname = nickname;
            this.password = password;
            this.email = email;
            this.admin = admin;
            this.group = group;
        }
        public Character(string nickname, string password, string email, bool admin, string group)
        {
            this.nickname = nickname;
            this.password = password;
            this.email = email;
            this.admin = admin;
            this.group = group;
        }
    }
}
