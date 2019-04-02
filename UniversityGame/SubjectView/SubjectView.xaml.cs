using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UniversityGame.SubjectView
{
    public partial class SubjectView : Page
    {
        private NpgsqlConnection sql;
        public SubjectView()
        {
            InitializeComponent();
            Connection conn = new Connection();
            sql = conn.getConnection();
            sql.Open();
            AddFromDatabase();
        }

        private bool checkColumns()
        {
            if (!Regex.IsMatch(nameField.ToString(), "[\\w ]{1,50}")) return false;
            if (!Regex.IsMatch(lectField.ToString(), "\\d+")) return false;
            if (!Regex.IsMatch(practField.ToString(), "\\d+")) return false;
            if (!Regex.IsMatch(labField.ToString(), "\\d+")) return false;
            if (!Regex.IsMatch(semField.ToString(), "\\d|\\d[\\d,]+\\d") || semField.Text.StartsWith(",")) return false;
            return true;
        }
        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            if (checkColumns())
            {

            }
            else MessageBox.Show("false");
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void updateButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void updateField_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddFromDatabase()
        {
            using (var com = new NpgsqlCommand("select * from university.subject", sql))
            using (var reader = com.ExecuteReader())
                while (reader.Read()) MessageBox.Show(reader[0].ToString() + reader[1].ToString() + reader[2].ToString() + reader[3].ToString() + reader[0].ToString());
        }
    }
}
