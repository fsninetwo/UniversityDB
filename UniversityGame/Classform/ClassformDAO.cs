using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace UniversityGame.Classform
{
    class ClassformDAO : DAO<Classform>
    {
        private NpgsqlConnection sql;

        public ClassformDAO()
        {
            Connection conn = new Connection();
            sql = conn.getConnection();
            sql.Open();
        }
        public void Insert(Classform item)
        {
            ExecuteQuery("insert into university.classform(name) values(@a);", item);
        }
        public void UpdateById(Classform item)
        {
            ExecuteQuery("update university.classform set name=@a where id=@b", item);
        }
        private void ExecuteQuery(string path, Classform item)
        {
            try
            {
                using (var com = new NpgsqlCommand(path, sql))
                {
                    com.Parameters.AddWithValue("a", item.name);
                    if (path.StartsWith("update")) com.Parameters.AddWithValue("b", item.id);
                    com.ExecuteNonQuery();
                }
            }
            catch (NpgsqlException e)
            {
                MessageBox.Show("Error: " + e.Message, "SQL Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public void DeleteById(Classform item)
        {
            try
            {
                using (var com = new NpgsqlCommand("delete from university.classform where id = @a;", sql))
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
        public int SelectId(string name, string path)
        {
            throw new NotImplementedException();
        }
        public List<string> SelectToComboBox(string path)
        {
            throw new NotImplementedException();
        }
        public List<Classform> SelectItems()
        {
            List<Classform> items = new List<Classform>();
            try
            {
                
                using (var com = new NpgsqlCommand("select * from university.classform", sql))
                using (var reader = com.ExecuteReader())
                    while (reader.Read()) items.Add(new Classform(reader.GetInt32(0), reader.GetString(1)));
            }
            catch (NpgsqlException e)
            {
                MessageBox.Show("Error: " + e.Message, "SQL Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return items;
        }

        public List<Classform> SelectItemsByText(string text)
        {
            List<Classform> items = new List<Classform>();
            try
            {

                using (var com = new NpgsqlCommand("select * from university.classform_filter (@a)", sql))
                {
                    com.Parameters.AddWithValue("a", text);
                    using (var reader = com.ExecuteReader())
                        while (reader.Read()) items.Add(new Classform(reader.GetInt32(0), reader.GetString(1)));
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
