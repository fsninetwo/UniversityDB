using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace UniversityGame.Subject
{
    class SubjectDAO : DAO<Subject>
    {
        private NpgsqlConnection sql;
        public SubjectDAO()
        {
            Connection conn = new Connection();
            sql = conn.getConnection();
            sql.Open();
        }
        public void Insert(Subject item)
        {
            ExecuteQuery("insert into university.subject(name, lections, practical, labratory, semestors) values(@a, @b, @c, @d, @e);", item);
        }
        public void UpdateById(Subject item)
        {
            ExecuteQuery("update university.subject set name=@a, lections=@b, practical=@c, labratory=@d, semestors=@e where id=@f", item);
        }
        private void ExecuteQuery(string path, Subject item)
        {
            try
            {
                using (var com = new NpgsqlCommand(path, sql))
                {
                    com.Parameters.AddWithValue("a", item.name);
                    com.Parameters.AddWithValue("b", item.lections);
                    com.Parameters.AddWithValue("c", item.practical);
                    com.Parameters.AddWithValue("d", item.labratory);
                    com.Parameters.AddWithValue("e", ConvertToArray(item.semestors));
                    if (path.StartsWith("update")) com.Parameters.AddWithValue("f", item.id);
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
            throw new NotImplementedException();
        }
        public int[] ConvertToArray(string text)
        {
            string[] sem = text.Split(',');
            int[] semestors = new int[sem.Length];
            for (int i = 0; i < sem.Length; i++)
                semestors[i] = Convert.ToInt32(sem[i]);
            return semestors;
        }
        public void DeleteById(Subject item)
        {
            try
            {
                using (var com = new NpgsqlCommand("delete from university.subject where id = @a;", sql))
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
            throw new NotImplementedException();
        }
        public List<Subject> SelectItems()
        {
            List<Subject> items = new List<Subject>();
            try
            {
                using (var com = new NpgsqlCommand("select * from university.subject", sql))
                using (var reader = com.ExecuteReader())
                    while (reader.Read()) items.Add(new Subject(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2), reader.GetInt32(3), reader.GetInt32(4), string.Join(",", (reader[5] as int[]))));
            }
            catch (NpgsqlException e)
            {
                MessageBox.Show("Error: " + e.Message, "SQL Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return items;
        }
        public List<Subject> SelectItemsByText(string text)
        {
            List<Subject> items = new List<Subject>();
            try
            {
                using (var com = new NpgsqlCommand("select * from university.subject_filter (@a)" , sql))
                {
                    com.Parameters.AddWithValue("a", text);
                    using (var reader = com.ExecuteReader())
                        while (reader.Read()) items.Add(new Subject(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2), reader.GetInt32(3), reader.GetInt32(4), string.Join(",", (reader[5] as int[]))));
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
