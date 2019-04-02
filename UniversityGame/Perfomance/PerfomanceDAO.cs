using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace UniversityGame.Perfomance
{
    class PerfomanceDAO : DAO<Perfomance>
    {
        private NpgsqlConnection sql;
        public PerfomanceDAO()
        {
            Connection conn = new Connection();
            sql = conn.getConnection();
            sql.Open();
        }
        public void DeleteById(Perfomance item)
        {
            try
            {
                using (var com = new NpgsqlCommand("delete from university.perfomance where id = @a;", sql))
                {
                    com.Parameters.AddWithValue("a", Convert.ToInt32(item.id));
                    com.ExecuteNonQuery();
                }
            }
            catch (NpgsqlException e)
            {
                MessageBox.Show("Error: " + e.Message, "SQL Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public void Insert(Perfomance item)
        {
            ExecuteQuery("insert into university.perfomance(mark, fk_character, fk_subject) values(@a, @b, @c); ", item);
        }
        public void UpdateById(Perfomance item)
        {
            ExecuteQuery("update university.perfomance set mark=@a, fk_character=@b, fk_subject=@c where id=@d", item);
        }
        private void ExecuteQuery(string path, Perfomance item)
        {
            try
            {
                using (var com = new NpgsqlCommand(path, sql))
                {
                    com.Parameters.AddWithValue("a", item.mark);
                    com.Parameters.AddWithValue("b", SelectId(item.character, "character"));
                    com.Parameters.AddWithValue("c", SelectId(item.subject, "subject"));
                    if (path.StartsWith("update")) com.Parameters.AddWithValue("d", item.id);
                    com.ExecuteNonQuery();
                }
            }
            catch (NpgsqlException e)
            {
                MessageBox.Show("Error: " + e.Message, "SQL Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private int SelectCharacterId(string name)
        {
            try
            {
                using (var com = new NpgsqlCommand("select id from university.character where nickname = @a", sql))
                {
                    com.Parameters.AddWithValue("a", name);
                    using (var reader = com.ExecuteReader())
                        while (reader.Read()) return reader.GetInt32(0);
                }
            }
            catch (NpgsqlException e)
            {
                MessageBox.Show("Error: " + e.Message, "SQL Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return 0;
        }
        private int SelectSubjectId(string name)
        {
            try
            {
                using (var com = new NpgsqlCommand("select a.id from university.semestor_subject as a inner join university.subject as b on (a.fk_subject = b.id) where b.name = @a and a.fk_semestor = @b", sql))
                {
                    string[] names = name.Split(' ');
                    com.Parameters.AddWithValue("a", names[0]);
                    com.Parameters.AddWithValue("b", Convert.ToInt32(names[1]));
                    using (var reader = com.ExecuteReader())
                        while (reader.Read()) return reader.GetInt32(0);
                }
            }
            catch (NpgsqlException e)
            {
                MessageBox.Show("Error: " + e.Message, "SQL Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return 0;
        }
        public int SelectId(string name, string path)
        {
            switch (path)
            {
                case "character": return SelectCharacterId(name);
                case "subject": return SelectSubjectId(name);
            }
            return 0;
        }
        private List<string> AddToCharacterBox()
        {
            List<string> items = new List<string>();
            try
            {
                using (var com = new NpgsqlCommand("select nickname from university.character", sql))
                using (var reader = com.ExecuteReader())
                    while (reader.Read()) items.Add(reader.GetString(0));
            }
            catch (NpgsqlException e)
            {
                MessageBox.Show("Error: " + e.Message, "SQL Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return items;
        }
        private List<string> AddToSubjectBox()
        {
            List<string> items = new List<string>();
            try
            {
                using (var com = new NpgsqlCommand("select a.name, b.fk_semestor from university.semestor_subject as b inner join university.subject as a on (b.fk_subject = a.id)", sql))
                using (var reader = com.ExecuteReader())
                    while (reader.Read()) items.Add(reader.GetString(0) + " " + reader.GetInt32(1));
            }
            catch (NpgsqlException e)
            {
                MessageBox.Show("Error: " + e.Message, "SQL Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return items;
        }
        public List<string> SelectToComboBox(string path)
        {
            switch(path)
            {
                case "character": return AddToCharacterBox();
                case "subject": return AddToSubjectBox();
            }
            return null;
        }
        public List<Perfomance> SelectItems()
        {
            List<Perfomance> items = new List<Perfomance>();
            try
            {
                using (var com = new NpgsqlCommand("select a.id, a.mark, b.nickname, d.name, c.fk_semestor from university.perfomance as a inner join university.character as b on (a.fk_character = b.id) inner join university.semestor_subject as c on (a.fk_subject = c.id) inner join university.subject as d on (c.fk_subject = d.id)", sql))
                using (var reader = com.ExecuteReader())
                    while (reader.Read()) items.Add(new Perfomance(reader.GetInt32(0), reader.GetInt32(1), reader.GetString(2), reader.GetString(3) + " " + reader.GetInt32(4)));
            }
            catch (NpgsqlException e)
            {
                MessageBox.Show("Error: " + e.Message, "SQL Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return items;
        }

        public List<Perfomance> SelectItemsByText(string text)
        {
            List<Perfomance> items = new List<Perfomance>();
            try
            {
                using (var com = new NpgsqlCommand("select * from university.performance_filter (@a)", sql))
                {
                    com.Parameters.AddWithValue("a", text);
                    using (var reader = com.ExecuteReader())
                        while (reader.Read()) items.Add(new Perfomance(reader.GetInt32(0), reader.GetInt32(1), reader.GetString(2), reader.GetString(3) + " " + reader.GetInt32(4)));
                }
            }
            catch (NpgsqlException e)
            {
                MessageBox.Show("Error: " + e.Message, "SQL Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return items;
        }
    }
}
