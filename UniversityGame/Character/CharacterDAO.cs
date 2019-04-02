using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace UniversityGame.Character
{
    class CharacterDAO : DAO<Character>
    {
        private NpgsqlConnection sql;
        public CharacterDAO()
        {
            Connection conn = new Connection("postgres", "");
            sql = conn.getConnection();
            sql.Open();
        }
        public void Insert(Character item)
        {
            ExecuteQuery("insert into university.character(nickname, password, email, admin, fk_group) values(@a, md5(@b), @c, @d, @e);", item);
        }
        public void UpdateById(Character item)
        {
            ExecuteQuery("update university.character set nickname=@a, password=md5(@b), email=@c, admin=@d, fk_group=@e where id=@g", item);
        }
        private void ExecuteQuery(string path, Character item)
        {
            try
            {
                using (var com = new NpgsqlCommand(path, sql))
                {
                    com.Parameters.AddWithValue("a", item.nickname);
                    com.Parameters.AddWithValue("b", item.password);
                    com.Parameters.AddWithValue("c", item.email);
                    com.Parameters.AddWithValue("d", item.admin);
                    com.Parameters.AddWithValue("e", SelectId(item.group, "group"));
                    if (path.StartsWith("update")) com.Parameters.AddWithValue("g", item.id);
                    com.ExecuteNonQuery();
                }
            }
            catch (NpgsqlException e)
            {
                MessageBox.Show("Error: " + e.Message, "SQL Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public int SelectId(string name, string path)
        {
            switch (path)
            {
                case "group": return SelectGroupId(name);
            }
            return 0;
        }
        private int SelectGroupId(string name)
        {
            try
            {
                using (var com = new NpgsqlCommand("select id from university.group where name = @a", sql))
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
        public void DeleteById(Character item)
        {
            try
            {
                using (var com = new NpgsqlCommand("delete from university.character where id = @a;", sql))
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
        public List<string> SelectToComboBox(string path)
        {
            switch (path)
            {
                case "group": return AddToGroupChoice();
            }
            return null;
        }
        private List<string> AddToGroupChoice()
        {
            List<string> items = new List<string>();
            try
            {
                using (var com = new NpgsqlCommand("select name from university.group", sql))
                using (var reader = com.ExecuteReader())
                    while (reader.Read()) items.Add(reader.GetString(0));
            }
            catch (NpgsqlException e)
            {
                MessageBox.Show("Error: " + e.Message, "SQL Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return items;
        }
        public List<Character> SelectItems()
        {
            List<Character> items = new List<Character>();
            try
            {
                
                using (var com = new NpgsqlCommand("select a.id, a.nickname, a.password, a.email, a.registration, a.admin, b.name from university.character as a inner join university.group as b on (a.fk_group = b.id)", sql))
                using (var reader = com.ExecuteReader())
                    while (reader.Read()) items.Add(new Character(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetDateTime(4), reader.GetBoolean(5), reader.GetString(6)));
            }
            catch (NpgsqlException e)
            {
                MessageBox.Show("Error: " + e.Message, "SQL Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return items;
        }

        public List<Character> SelectItemsByText(string text)
        {
            List<Character> items = new List<Character>();
            try
            {

                using (var com = new NpgsqlCommand("select * from university.character_filter(@a);", sql))
                {
                    com.Parameters.AddWithValue("a", text);
                    using (var reader = com.ExecuteReader())
                        while (reader.Read()) items.Add(new Character(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetDateTime(4), reader.GetBoolean(5), reader.GetString(6)));
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
