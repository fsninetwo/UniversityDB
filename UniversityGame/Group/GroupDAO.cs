using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace UniversityGame.Group
{
    class GroupDAO : DAO<Group>
    {
        private NpgsqlConnection sql;

        public GroupDAO()
        {
            Connection conn = new Connection();
            sql = conn.getConnection();
            sql.Open();
        }
        public void Insert(Group item)
        {
            ExecuteQuery("insert into university.group(name, fk_department) values(@a, @b);", item);
        }
        public void UpdateById(Group item)
        {
            ExecuteQuery("update university.group set name=@a, fk_department=@b where id=@c", item);
        }
        private void ExecuteQuery(string path, Group item)
        {
            try
            {
                using (var com = new NpgsqlCommand(path, sql))
                {
                    com.Parameters.AddWithValue("a", item.name);
                    com.Parameters.AddWithValue("b", SelectId(item.department, "department"));
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
            switch(path)
            {
                case "department": return selectDepartmentId(name);
            }
            return 0;
        }
        private int selectDepartmentId(string name)
        {
            try
            {
                using (var com = new NpgsqlCommand("select id from university.department where name = @a", sql))
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
        public void DeleteById(Group item)
        {
            try
            {
                using (var com = new NpgsqlCommand("delete from university.group where id = @a;;", sql))
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
                case "department": return AddToDepartmentBox();
            }
            return null;
        }
        private List<string> AddToDepartmentBox()
        {
            try
            {
                List<string> items = new List<string>();
                using (var com = new NpgsqlCommand("select name from university.department", sql))
                using (var reader = com.ExecuteReader())
                    while (reader.Read()) items.Add(reader.GetString(0));
                return items;
            }
            catch (NpgsqlException e)
            {
                MessageBox.Show("Error: " + e.Message, "SQL Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return null;
        }
        public List<Group> SelectItems()
        {
            List<Group> items = new List<Group>();
            try
            {
                using (var com = new NpgsqlCommand("select a.id, a.name, b.name from university.group as a inner join university.department as b on (a.fk_department = b.id)", sql))
                using (var reader = com.ExecuteReader())
                    while (reader.Read()) items.Add(new Group(reader.GetInt32(0), reader.GetString(1), reader.GetString(2)));
            }
            catch (NpgsqlException e)
            {
                MessageBox.Show("Error: " + e.Message, "SQL Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return items;
        }
        public List<Group> SelectItemsByText(string text)
        {
            List<Group> items = new List<Group>();
            try
            {
                using (var com = new NpgsqlCommand("select * from university.group_filter (@a) ", sql))
                {
                    com.Parameters.AddWithValue("a", text);
                    using (var reader = com.ExecuteReader())
                        while (reader.Read()) items.Add(new Group(reader.GetInt32(0), reader.GetString(1), reader.GetString(2)));
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
