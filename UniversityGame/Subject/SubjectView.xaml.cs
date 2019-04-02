using Microsoft.Office.Interop.Excel;
using Microsoft.Win32;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace UniversityGame.Subject
{
    /// <summary>
    /// Interaction logic for SubjectView.xaml
    /// </summary>
    public partial class SubjectView : UserControl
    {
        private DAO<Subject> dao;
        private int id = 0;
        private string search;
        public SubjectView()
        {
            InitializeComponent();
            dao = new SubjectDAO();
            Initialize("");
        }

        public void Initialize(string text)
        {
            search = text;
            AddFromDatabase();
        }

        private bool checkColumns()
        {
            if (!Regex.IsMatch(nameField.ToString(), "[\\w ]{1,50}"))
            {
                MessageBox.Show("Cabinet field must have letters, numbers and doesn't have more than 50 symbols!", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (!Regex.IsMatch(lectField.Text, "\\d+"))
            {
                MessageBox.Show("Lecture field must have a numeric value!", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (!Regex.IsMatch(practField.Text, "\\d+"))
            {
                MessageBox.Show("Practical field must have a numeric value!", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (!Regex.IsMatch(labField.Text, "\\d+"))
            {
                MessageBox.Show("Labratory field must have a numeric value!", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (!Regex.IsMatch(semField.ToString(), "\\d|\\d[\\d,]+\\d"))
            {
                MessageBox.Show("Semestor field must have a numeric value or some numerics threw comma!", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }
        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            if (checkColumns())
            {
                dao.Insert(new Subject(nameField.Text, Convert.ToInt32(lectField.Text), Convert.ToInt32(practField.Text), Convert.ToInt32(labField.Text), semField.Text));
                AddFromDatabase();
            }
            CleanFields();
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            dao.DeleteById((dynamic)subjectTable.SelectedItem);
            AddFromDatabase();
        }

        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            if (checkColumns() && id != 0)
            {
                dao.UpdateById(new Subject(id, nameField.Text, Convert.ToInt32(lectField.Text), Convert.ToInt32(practField.Text), Convert.ToInt32(labField.Text), semField.Text));
                AddFromDatabase();
            }
            CleanFields();
        }

        private void updateField_Click(object sender, RoutedEventArgs e)
        {
            AddFromDatabase();
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel Workbook|*.xlsx";
            sfd.ValidateNames = true;
            sfd.ShowDialog();
            if (!sfd.FileName.Equals(""))
            {
                Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
                Workbook wb = app.Workbooks.Add(XlSheetType.xlWorksheet);
                Worksheet ws = app.ActiveSheet;
                app.Visible = false;
                for (int i = 0; i < subjectTable.Items.Count; i++)
                {
                    subjectTable.SelectedIndex = i;
                    Subject selectedItem = (dynamic)subjectTable.SelectedItem;
                    ws.Cells[i + 1, 1] = selectedItem.id.ToString();
                    ws.Cells[i + 1, 2] = selectedItem.name.ToString();
                    ws.Cells[i + 1, 3] = selectedItem.lections.ToString();
                    ws.Cells[i + 1, 4] = selectedItem.practical.ToString();
                    ws.Cells[i + 1, 5] = selectedItem.labratory.ToString();
                    ws.Cells[i + 1, 6] = selectedItem.semestors.ToString();
                }
                wb.SaveAs(sfd.FileName, XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing, false, false, XlSaveAsAccessMode.xlNoChange, XlSaveConflictResolution.xlLocalSessionChanges, Type.Missing, Type.Missing, true, false);
                app.Quit();
            }
        }

        public void CleanFields()
        {
            nameField.Text = "";
            lectField.Text = "";
            practField.Text = "";
            labField.Text = "";
            semField.Text = "";
            id = 0;
        }

        private void AddFromDatabase()
        {
            subjectTable.Items.Clear();
            List<Subject> items;
            if (search.Equals("")) items = dao.SelectItems();
            else items = dao.SelectItemsByText(search);
            foreach (Subject item in items) subjectTable.Items.Add(item);
        }

        private void subjectTable_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (subjectTable.SelectedIndex >= 0)
            {
                var selectedItem = (dynamic)subjectTable.SelectedItem;
                nameField.Text = selectedItem.name;
                lectField.Text = selectedItem.lections.ToString();
                practField.Text = selectedItem.practical.ToString();
                labField.Text = selectedItem.labratory.ToString();
                semField.Text = selectedItem.semestors;
                id = selectedItem.id;
            }
        }

        private void subjectTable_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            CleanFields();
        }
    }
}
