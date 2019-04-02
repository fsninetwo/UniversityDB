using Npgsql;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityGame
{
    class Connection
    {
        /*private string host = "localhost";
        private string port = "5432";
        private string username = "postgres";
        private string password = "";
        private string database = "app";*/
        private string host;
        private string port;
        private string username;
        private string password;
        private string database;
        private NpgsqlConnection conn;

        public Connection()
        {
            string[] st;
            using (StreamReader load = new StreamReader("file.txt")) st = load.ReadLine().Split(',');
            this.host = st[0];
            this.port = st[1];
            this.username = st[2];
            this.password = st[3];
            this.database = st[4];
            Initialize();
        }

        public Connection(string username, string password)
        {
            string[] st;
            using (StreamReader load = new StreamReader("file.txt")) st = load.ReadLine().Split(',');                
            this.host = st[0];
            this.port = st[1];
            this.username = username;
            this.password = password;
            this.database = st[4];
            Initialize();
        }

        public Connection(string host, string port, string username, string password, string database)
        {
            this.host = host;
            this.port = port;
            this.username = username;
            this.password = password;
            this.database = database;
            Initialize();
        }

        private void Initialize()
        {
            conn = new NpgsqlConnection(string.Format("Host={0}; Port={1};Username={2};Password={3};Database={4}", host, port, username, password, database));
        }

        public void SaveData()
        {
            using (StreamWriter save = new StreamWriter("file.txt")) save.WriteLine("{0},{1},{2},{3},{4}", host, port, username, password, database);
        }
        public NpgsqlConnection getConnection()
        {
            return conn;
        }
    }
}
