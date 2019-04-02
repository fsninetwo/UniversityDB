using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace UniversityGame.Subject_Structure
{
    class SubjectStructureDAO : DAO<SubjectStructure>
    {
        private NpgsqlConnection sql;
        public SubjectStructureDAO()
        {
            Connection conn = new Connection();
            sql = conn.getConnection();
            sql.Open();
        }
        public void DeleteById(SubjectStructure item)
        {
            try
            {
                using (var com = new NpgsqlCommand("delete from university.subject_structure where id = @a;", sql))
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
        public void Insert(SubjectStructure item)
        {
            ExecuteQuery("insert into university.subject_structure(count, fk_classform, fk_subject) values(@a, @b, @c);", item);
        }
        public void UpdateById(SubjectStructure item)
        {
            ExecuteQuery("update university.subject_structure set count=@a, fk_classform=@b, fk_subject=@c where id=@d", item);
        }
        private void ExecuteQuery(string path, SubjectStructure item)
        {
            try
            {
                using (var com = new NpgsqlCommand(path, sql))
                {
                    com.Parameters.AddWithValue("a", item.count);
                    com.Parameters.AddWithValue("b", SelectId(item.classform, "classform"));
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
        private int SelectClassformId(string name)
        {
            try
            {
                using (var com = new NpgsqlCommand("select id from university.classform where name = @a", sql))
                {
                    com.Parameters.AddWithValue("a", name);
                    using (var reader = com.ExecuteReader())
                        while (reader.Read()) return reader.GetInt32(0);
                }
            }
            catch (Exception e)
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
            catch (Exception e)
            {
                MessageBox.Show("Error: " + e.Message, "SQL Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return 0;
        }

        public int SelectId(string name, string path)
        {
            switch (path)
            {
                case "classform": return SelectClassformId(name);
                case "subject": return SelectSubjectId(name);
            }
            return 0;
        }
        private List<string> AddToClassformBox()
        {
            List<string> items = new List<string>();
            try
            {
                using (var com = new NpgsqlCommand("select name from university.classform", sql))
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
                case "classform": return AddToClassformBox();
                case "subject": return AddToSubjectBox();
            }
            return null;
        }
        public List<SubjectStructure> SelectItems()
        {
            List<SubjectStructure> items = new List<SubjectStructure>();
            try
            {
                using (var com = new NpgsqlCommand("select a.id, a.count, b.name, d.name, c.fk_semestor from university.subject_structure as a inner join university.classform as b on (a.fk_classform = b.id) inner join university.semestor_subject as c on (a.fk_subject = c.id) inner join university.subject as d on (c.fk_subject = d.id)", sql))
                using (var reader = com.ExecuteReader())
                    while (reader.Read()) items.Add(new SubjectStructure(reader.GetInt32(0), reader.GetInt32(1), reader.GetString(2), reader.GetString(3) + " " + reader.GetInt32(4)));
            }
            catch (NpgsqlException e)
            {
                MessageBox.Show("Error: " + e.Message, "SQL Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return items;
        }

        public List<SubjectStructure> SelectItemsByText(string text)
        {
            List<SubjectStructure> items = new List<SubjectStructure>();
            try
            {
                using (var com = new NpgsqlCommand("select * from university.subject_structure_filter (@a)", sql))
                {
                    com.Parameters.AddWithValue("a", text);
                    using (var reader = com.ExecuteReader())
                        while (reader.Read()) items.Add(new SubjectStructure(reader.GetInt32(0), reader.GetInt32(1), reader.GetString(2), reader.GetString(3) + " " + reader.GetInt32(4)));
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
