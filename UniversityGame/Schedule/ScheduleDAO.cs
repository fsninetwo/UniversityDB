using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace UniversityGame.Schedule
{
    class ScheduleDAO : DAO<Schedule>
    {
        private NpgsqlConnection sql;
        public ScheduleDAO()
        {
            Connection conn = new Connection();
            sql = conn.getConnection();
            sql.Open();
        }
        public void DeleteById(Schedule item)
        {
            try
            {
                using (var com = new NpgsqlCommand("delete from university.schedule where id = @a;", sql))
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
        public void Insert(Schedule item)
        {
            ExecuteQuery("insert into university.schedule(day, start, finish, cabinet, fk_group, fk_structure) values(@a, @b, @c, @d, @e, @f);", item);
        }
        public void UpdateById(Schedule item)
        {
            ExecuteQuery("update university.schedule set day=@a, start=@b, finish=@c, cabinet=@d, fk_group=@e, fk_structure=@f where id=@g", item);
        }
        private void ExecuteQuery(string path, Schedule item)
        {
            try
            {
                using (var com = new NpgsqlCommand(path, sql))
                {
                    com.Parameters.AddWithValue("a", item.day);
                    com.Parameters.AddWithValue("b", item.start);
                    com.Parameters.AddWithValue("c", item.finish);
                    com.Parameters.AddWithValue("d", item.cabinet);
                    com.Parameters.AddWithValue("e", SelectId(item.group, "group"));
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
                case "group": return SelectGroupId(name);
                case "subject": return SelectSubjectId(name);
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
        private int SelectSubjectId(string name)
        {
            try
            {
                using (var com = new NpgsqlCommand("select structure.id from university.subject_structure as structure inner join university.semestor_subject as semestor on (structure.fk_subject = semestor.id) inner join university.subject as subject on (semestor.fk_subject = subject.id) inner join university.classform as classform on (structure.fk_classform = classform.id) where subject.name = @a and semestor.fk_semestor = @b and classform.name = @c;", sql))
                {
                    string[] names = name.Split(' ');
                    com.Parameters.AddWithValue("a", names[0]);
                    com.Parameters.AddWithValue("b", Convert.ToInt32(names[1]));
                    com.Parameters.AddWithValue("c", names[2]);
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
                case "group": return AddToGroupBox();
                case "subject": return AddToSubjectBox();
            }
            return null;
        }
        private List<string> AddToGroupBox()
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
        private List<string> AddToSubjectBox()
        {
            List<string> items = new List<string>();
            try
            {
                using (var com = new NpgsqlCommand("select subject.name, semestor.fk_semestor, classform.name from university.subject_structure as structure inner join university.semestor_subject as semestor on (structure.fk_subject = semestor.id) inner join university.subject as subject on (semestor.fk_subject = subject.id) inner join university.classform as classform on (structure.fk_classform = classform.id);", sql))
                using (var reader = com.ExecuteReader())
                    while (reader.Read()) items.Add(reader.GetString(0) + " " + reader.GetInt32(1) + " " + reader.GetString(2));
            }
            catch (NpgsqlException e)
            {
                MessageBox.Show("Error: " + e.Message, "SQL Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return items;
        }
        public List<Schedule> SelectItems()
        {
            List<Schedule> items = new List<Schedule>();
            try
            {
                using (var com = new NpgsqlCommand("select a.id, a.day, a.start, a.finish, a.cabinet, b.name, subject.name, semestor.fk_semestor, classform.name from university.schedule as a inner join university.group as b on (a.fk_group = b.id) inner join university.subject_structure as structure on (a.fk_structure = structure.id) inner join university.semestor_subject as semestor on (structure.fk_subject = semestor.id) inner join university.subject as subject on (semestor.fk_subject = subject.id) inner join university.classform as classform on (structure.fk_classform = classform.id);", sql))
                using (var reader = com.ExecuteReader())
                    while (reader.Read()) items.Add(new Schedule(reader.GetInt32(0), reader.GetInt32(1), reader.GetTimeSpan(2), reader.GetTimeSpan(3), reader.GetString(4), reader.GetString(5), reader.GetString(6) + " " + reader.GetInt32(7) + " " + reader.GetString(8)));
            }
            catch (NpgsqlException e)
            {
                MessageBox.Show("Error: " + e.Message, "SQL Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return items;
        }

        public List<Schedule> SelectItemsByText(string text)
        {
            List<Schedule> items = new List<Schedule>();
            try
            {
                using (var com = new NpgsqlCommand("select * from university.schedule_filter (@a) ", sql))
                {
                    com.Parameters.AddWithValue("a", text);
                    using (var reader = com.ExecuteReader())
                        while (reader.Read()) items.Add(new Schedule(reader.GetInt32(0), reader.GetInt32(1), reader.GetTimeSpan(2), reader.GetTimeSpan(3), reader.GetString(4), reader.GetString(5), reader.GetString(6) + " " + reader.GetInt32(7) + " " + reader.GetString(8)));
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
