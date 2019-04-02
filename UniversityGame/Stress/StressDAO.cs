using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace UniversityGame.Stress
{
    class StressDAO : DAO<Stress>
    {
        private NpgsqlConnection sql;
        public StressDAO()
        {
            Connection conn = new Connection();
            sql = conn.getConnection();
            sql.Open();
        }
        public void DeleteById(Stress item)
        {
            try
            {
                using (var com = new NpgsqlCommand("delete from university.stress where id = @a;", sql))
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
        public void Insert(Stress item)
        {
            ExecuteQuery("insert into university.stress(day, stress, fk_character) values(@a, @b, @c);", item);
        }
        public void UpdateById(Stress item)
        {
            ExecuteQuery("update university.stress set day=@a, stress=@b, fk_character=@c where id=@d", item);
        }
        private void ExecuteQuery(string path, Stress item)
        {
            try
            {
                using (var com = new NpgsqlCommand(path, sql))
                {
                    com.Parameters.AddWithValue("a", item.day);
                    com.Parameters.AddWithValue("b", item.stress);
                    com.Parameters.AddWithValue("c", SelectId(item.nickname, "character"));
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
        public int SelectId(string name, string path)
        {
            switch (path)
            {
                case "character": return SelectCharacterId(name);
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
        public List<string> SelectToComboBox(string path)
        {
            switch(path)
            {
                case "character": return AddToCharacterBox();
            }
            return null;
        }
        public List<Stress> SelectItems()
        {
            List<Stress> items = new List<Stress>();
            try
            {
                using (var com = new NpgsqlCommand("select a.id, a.day, a.stress, b.nickname from university.stress as a inner join university.character as b on(a.fk_character = b.id)", sql))
                using (var reader = com.ExecuteReader())
                    while (reader.Read()) items.Add(new Stress(reader.GetInt32(0), reader.GetDateTime(1), reader.GetInt32(2), reader.GetString(3)));
            }
            catch (NpgsqlException e)
            {
                MessageBox.Show("Error: " + e.Message, "SQL Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return items;
        }

        public List<Stress> SelectItemsByText(string text)
        {
            List<Stress> items = new List<Stress>();
            try
            {
                using (var com = new NpgsqlCommand("select * from university.stress_filter (@a)", sql))
                {
                    com.Parameters.AddWithValue("a", text);
                    using (var reader = com.ExecuteReader())
                        while (reader.Read()) items.Add(new Stress(reader.GetInt32(0), reader.GetDateTime(1), reader.GetInt32(2), reader.GetString(3)));
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
