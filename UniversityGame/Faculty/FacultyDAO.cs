using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace UniversityGame.Faculty
{
    class FacultyDAO : DAO<Faculty>
    {
        private NpgsqlConnection sql;

        public FacultyDAO()
        {
            Connection conn = new Connection();
            sql = conn.getConnection();
            sql.Open();
        }
        public void Insert(Faculty item)
        {
            ExecuteQuery("insert into university.faculty(name, description) values(@a, @b);", item);
        }
        public void UpdateById(Faculty item)
        {
            ExecuteQuery("update university.classform set name=@a, description=@b where id=@c", item);
        }
        private void ExecuteQuery(string path, Faculty item)
        {
            try
            {
                using (var com = new NpgsqlCommand(path, sql))
                {
                    com.Parameters.AddWithValue("a", item.name);
                    com.Parameters.AddWithValue("b", item.description);
                    if (path.StartsWith("update")) com.Parameters.AddWithValue("c", item.id);
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
        public void DeleteById(Faculty item)
        {
            try
            {
                using (var com = new NpgsqlCommand("delete from university.faculty where id = @a;", sql))
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
        public List<Faculty> SelectItems()
        {
            List<Faculty> items = new List<Faculty>();
            try
            {
                
                using (var com = new NpgsqlCommand("select * from university.faculty", sql))
                using (var reader = com.ExecuteReader())
                    while (reader.Read()) items.Add(new Faculty(reader.GetInt32(0), reader.GetString(1), reader.GetString(2)));
            }
            catch (NpgsqlException e)
            {
                MessageBox.Show("Error: " + e.Message, "SQL Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return items;
        }

        public List<Faculty> SelectItemsByText(string text)
        {
            List<Faculty> items = new List<Faculty>();
            try
            {
                using (var com = new NpgsqlCommand("select * from university.faculty_filter (@a) ", sql))
                {
                    com.Parameters.AddWithValue("a", text);
                    using (var reader = com.ExecuteReader())
                        while (reader.Read()) items.Add(new Faculty(reader.GetInt32(0), reader.GetString(1), reader.GetString(2)));
                }
            }
            catch (NpgsqlException e)
            {
                MessageBox.Show("Error: " + e.Message, "SQL Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return items;
        }

        public List<string> SelectToComboBox(string path)
        {
            throw new NotImplementedException();
        }
    }
}
