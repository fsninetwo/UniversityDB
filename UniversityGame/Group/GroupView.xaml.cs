using Microsoft.Office.Interop.Excel;
using Microsoft.Win32;
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

namespace UniversityGame.Group
{
    /// <summary>
    /// Interaction logic for GroupView.xaml
    /// </summary>
    public partial class GroupView : UserControl
    {
        private DAO<Group> dao;
        private int id = 0;
        private string search;
        public GroupView()
        {
            InitializeComponent();
            dao = new GroupDAO();
            Initialize("");
        }

        public void Initialize(string text)
        {
            search = text;
            AddFromDatabase();
            AddToDepartmentChoice();
        }
        private bool checkColumns()
        {
            if (!Regex.IsMatch(nameField.Text, "[\\w ]{1,40}"))
            {
                MessageBox.Show("Nickname field must have letters, numbers and doesn't have more than 40 symbols!", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (departmentChoice.SelectedItem == null)
            {
                MessageBox.Show("Group box must have a value!", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }
        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            if (checkColumns())
            {
                dao.Insert(new Group(nameField.Text, departmentChoice.SelectedItem.ToString()));
                AddFromDatabase();
            }
            CleanFields();
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            dao.DeleteById((dynamic)groupTable.SelectedItem);
            AddFromDatabase();
        }

        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            if (checkColumns() && id != 0)
            {
                dao.UpdateById(new Group(id, nameField.Text, departmentChoice.SelectedItem.ToString()));
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
                for (int i = 0; i < groupTable.Items.Count; i++)
                {
                    groupTable.SelectedIndex = i;
                    Group selectedItem = (dynamic)groupTable.SelectedItem;
                    ws.Cells[i + 1, 1] = selectedItem.id.ToString();
                    ws.Cells[i + 1, 2] = selectedItem.name.ToString();
                    ws.Cells[i + 1, 3] = selectedItem.department.ToString();
                }
                wb.SaveAs(sfd.FileName, XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing, false, false, XlSaveAsAccessMode.xlNoChange, XlSaveConflictResolution.xlLocalSessionChanges, Type.Missing, Type.Missing, true, false);
                app.Quit();
            }
        }

        public void CleanFields()
        {
            nameField.Text = "";
            departmentChoice.SelectedItem = null;
            id = 0;
        }

        private void AddFromDatabase()
        {
            groupTable.Items.Clear();
            List<Group> items;
            if (search.Equals("")) items = dao.SelectItems();
            else items = dao.SelectItemsByText(search);
            foreach(Group item in items) groupTable.Items.Add(item);
        }

        private void AddToDepartmentChoice()
        {
            departmentChoice.Items.Clear();
            List<string> items = dao.SelectToComboBox("department");
            foreach (string item in items) departmentChoice.Items.Add(item);
        }

        private void subjectTable_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (groupTable.SelectedIndex >= 0)
            {
                var selectedItem = (dynamic)groupTable.SelectedItem;
                nameField.Text = selectedItem.name;
                departmentChoice.SelectedItem = selectedItem.department;
                id = selectedItem.id;
            }
        }

        private void subjectTable_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            CleanFields();
        }
    }
}
