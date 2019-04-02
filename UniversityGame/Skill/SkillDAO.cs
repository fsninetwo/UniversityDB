using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace UniversityGame.Skill
{
    class SkillDAO : DAO<Skill>
    {
        private NpgsqlConnection sql;
        public SkillDAO()
        {
            Connection conn = new Connection();
            sql = conn.getConnection();
            sql.Open();
        }
        public void DeleteById(Skill item)
        {
            try
            {
                using (var com = new NpgsqlCommand("delete from university.skill where id = @a;", sql))
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
        public void Insert(Skill item)
        {
            ExecuteQuery("insert into university.skill(condition, fk_character, fk_subject) values(@a, @b, @c);", item);
        }
        public void UpdateById(Skill item)
        {
            ExecuteQuery("update university.skill set condition=@a, fk_character=@b, fk_subject=@c where id=@d", item);
        }
        private void ExecuteQuery(string path, Skill item)
        {
            try
            {
                using (var com = new NpgsqlCommand(path, sql))
                {
                    com.Parameters.AddWithValue("a", item.condition);
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
                using (var com = new NpgsqlCommand("select id from university.subject where name = @a", sql))
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
                using (var com = new NpgsqlCommand("select name from university.subject", sql))
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
                case "subject": return AddToSubjectBox();
            }
            return null;
        }
        public List<Skill> SelectItems()
        {
            List<Skill> items = new List<Skill>();
            try
            {
                using (var com = new NpgsqlCommand("select a.id, a.condition, b.nickname, c.name from university.skill as a inner join university.character as b on (a.fk_character = b.id) inner join university.subject as c on (a.fk_subject = c.id)", sql))
                using (var reader = com.ExecuteReader())
                    while (reader.Read()) items.Add(new Skill(reader.GetInt32(0), reader.GetInt32(1), reader.GetString(2), reader.GetString(3)));
            }
            catch (NpgsqlException e)
            {
                MessageBox.Show("Error: " + e.Message, "SQL Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return items;
        }

        public List<Skill> SelectItemsByText(string text)
        {
            List<Skill> items = new List<Skill>();
            try
            {
                using (var com = new NpgsqlCommand("select * from university.skill_filter (@a) ", sql))
                {
                    com.Parameters.AddWithValue("a", text);
                    using (var reader = com.ExecuteReader())
                        while (reader.Read()) items.Add(new Skill(reader.GetInt32(0), reader.GetInt32(1), reader.GetString(2), reader.GetString(3)));
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
