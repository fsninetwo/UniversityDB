using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace UniversityGame.Department
{
    class DepartmentDAO : DAO<Department>
    {
        private NpgsqlConnection sql;

        public DepartmentDAO()
        {
            Connection conn = new Connection();
            sql = conn.getConnection();
            sql.Open();
        }
        public void Insert(Department item)
        {
            ExecuteQuery("insert into university.department(name, description, fk_faculty) values(@a, @b, @c);", item);
        }
        public void UpdateById(Department item)
        {
            ExecuteQuery("update university.department set name=@a, description=@b, fk_faculty_@c where id=@d", item);
        }
        private void ExecuteQuery(string path, Department item)
        {
            try
            {
                using (var com = new NpgsqlCommand(path, sql))
                {
                    com.Parameters.AddWithValue("a", item.name);
                    com.Parameters.AddWithValue("b", item.description);
                    com.Parameters.AddWithValue("c", SelectId(item.faculty, "faculty"));
                    if (path.StartsWith("update")) com.Parameters.AddWithValue("d", item.id);
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
                case "faculty": return SelectFacultyId(name);
            }
            return 0;
        }
        private int SelectFacultyId(string name)
        {
            try
            {
                using (var com = new NpgsqlCommand("select id from university.faculty where name = @a", sql))
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
        public void DeleteById(Department item)
        {
            try
            {
                using (var com = new NpgsqlCommand("delete from university.department where id = @a;", sql))
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
                case "faculty": return AddToFacultyComboBox();
            }
            return null;
        }

        private List<string> AddToFacultyComboBox()
        {
            try
            {
                List<string> items = new List<string>();
                using (var com = new NpgsqlCommand("select name from university.faculty", sql))
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
        public List<Department> SelectItems()
        {
            try
            {
                List<Department> items = new List<Department>();
                using (var com = new NpgsqlCommand("select department.id, department.name, department.description, faculty.name from university.department as department inner join university.faculty as faculty on (department.fk_faculty = faculty.id)", sql))
                using (var reader = com.ExecuteReader())
                    while (reader.Read()) items.Add(new Department(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3)));
                return items;
            }
            catch (NpgsqlException e)
            {
                MessageBox.Show("Error: " + e.Message, "SQL Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return null;
        }
        public List<Department> SelectItemsByText(string text)
        {
            try
            {
                List<Department> items = new List<Department>();
                using (var com = new NpgsqlCommand("select * from university.department_filter (@a) ", sql))
                {
                    com.Parameters.AddWithValue("a", text);
                    using (var reader = com.ExecuteReader())
                        while (reader.Read()) items.Add(new Department(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3)));
                }
                return items;
            }
            catch (NpgsqlException e)
            {
                MessageBox.Show("Error: " + e.Message, "SQL Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return null;
        }
    }
}
