using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace UniversityGame.Semestor_Subject
{
    class SemestorSubjectDAO : DAO<SemestorSubject>
    {
        private NpgsqlConnection sql;
        public SemestorSubjectDAO()
        {
            Connection conn = new Connection();
            sql = conn.getConnection();
            sql.Open();
        }
        public void DeleteById(SemestorSubject item)
        {
            try
            {
                using (var com = new NpgsqlCommand("delete from university.semestor_subject where id = @a;", sql))
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
        public void Insert(SemestorSubject item)
        {
            ExecuteQuery("insert into university.semestor_subject(lections, practical, labratory, response, fk_semestor, fk_subject) values(@a, @b, @c, @d, @e, @f);", item);
        }
        public void UpdateById(SemestorSubject item)
        {
            ExecuteQuery("update university.semestor_subject set lections=@a, practical=@b, labratory=@c, response=@d, fk_semestor=@e, fk_subject=@f where id=@g", item);
        }
        private void ExecuteQuery(string path, SemestorSubject item)
        {
            try
            {
                using (var com = new NpgsqlCommand(path, sql))
                {
                    com.Parameters.AddWithValue("a", item.lections);
                    com.Parameters.AddWithValue("b", item.practical);
                    com.Parameters.AddWithValue("c", item.labratory);
                    com.Parameters.AddWithValue("d", item.response);
                    com.Parameters.AddWithValue("e", item.semestor);
                    com.Parameters.AddWithValue("f", SelectId(item.subject, "subject"));
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
                case "subject": return SelectSubjectId(name);
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
        public List<string> SelectToComboBox(string path)
        {
            switch (path)
            {
                case "subject": return AddToSubjectBox();
            }
            return null;
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
        public List<SemestorSubject> SelectItems()
        {
            List<SemestorSubject> items = new List<SemestorSubject>();
            try
            {
                using (var com = new NpgsqlCommand("select a.id, a.lections, a.practical, a.labratory, a.response, a.fk_semestor, b.name from university.semestor_subject as a inner join university.subject as b on (a.fk_subject = b.id)", sql))
                using (var reader = com.ExecuteReader())
                    while (reader.Read()) items.Add(new SemestorSubject(reader.GetInt32(0), reader.GetInt32(1), reader.GetInt32(2), reader.GetInt32(3), reader.GetString(4), reader.GetInt32(5), reader.GetString(6)));
            }
            catch (NpgsqlException e)
            {
                MessageBox.Show("Error: " + e.Message, "SQL Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return items;
        }

        public List<SemestorSubject> SelectItemsByText(string text)
        {
            List<SemestorSubject> items = new List<SemestorSubject>();
            try
            {
                using (var com = new NpgsqlCommand("select * from university.semestor_subject_filter (@a) ", sql))
                {
                    com.Parameters.AddWithValue("a", text);
                    using (var reader = com.ExecuteReader())
                        while (reader.Read()) items.Add(new SemestorSubject(reader.GetInt32(0), reader.GetInt32(1), reader.GetInt32(2), reader.GetInt32(3), reader.GetString(4), reader.GetInt32(5), reader.GetString(6)));
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
