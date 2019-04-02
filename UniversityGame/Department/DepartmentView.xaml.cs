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

namespace UniversityGame.Department
{
    /// <summary>
    /// Interaction logic for DepartmentView.xaml
    /// </summary>
    public partial class DepartmentView : UserControl
    {
        private DAO<Department> dao;
        private int id = 0;
        private string search;
        public DepartmentView()
        {
            InitializeComponent();
            dao = new DepartmentDAO();
            Initialize("");
        }

        public void Initialize(string text)
        {
            search = text;
            AddFromDatabase();
            AddToFacultyChoice();
        }

        private bool checkColumns()
        {
            if (!Regex.IsMatch(nameField.Text, "[\\w ]{1,50}"))
            {
                MessageBox.Show("Nickname field must have letters, numbers and doesn't have more than 50 symbols!", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (!Regex.IsMatch(new TextRange(descriptionField.Document.ContentStart, descriptionField.Document.ContentEnd).Text, "[\\w ]{1,5000}"))
            {
                MessageBox.Show("Nickname field must have letters, numbers and doesn't have more than 5000 symbols!", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (facultyChoice.SelectedItem == null)
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
                dao.Insert(new Department(nameField.Text, new TextRange(descriptionField.Document.ContentStart, descriptionField.Document.ContentEnd).Text, facultyChoice.SelectedItem.ToString()));
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
                dao.UpdateById(new Department(id, nameField.Text, new TextRange(descriptionField.Document.ContentStart, descriptionField.Document.ContentEnd).Text, facultyChoice.SelectedItem.ToString()));
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
                    Department selectedItem = (dynamic)subjectTable.SelectedItem;
                    ws.Cells[i + 1, 1] = selectedItem.id.ToString();
                    ws.Cells[i + 1, 2] = selectedItem.name.ToString();
                    ws.Cells[i + 1, 3] = selectedItem.description.ToString();
                    ws.Cells[i + 1, 4] = selectedItem.faculty.ToString();
                }
                wb.SaveAs(sfd.FileName, XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing, false, false, XlSaveAsAccessMode.xlNoChange, XlSaveConflictResolution.xlLocalSessionChanges, Type.Missing, Type.Missing, true, false);
                app.Quit();
            }
        }

        public void AddToFacultyChoice()
        {
            facultyChoice.Items.Clear();
            List<string> items = dao.SelectToComboBox("faculty");
            foreach (string item in items) facultyChoice.Items.Add(item);
        }

        public void CleanFields()
        {
            nameField.Text = "";
            facultyChoice.SelectedItem = null;
            descriptionField.Document.Blocks.Add(new Paragraph(new Run(null)));
            id = 0;
        }

        private void AddFromDatabase()
        {
            subjectTable.Items.Clear();
            List<Department> items;
            if (search.Equals("")) items = dao.SelectItems();
            else items = dao.SelectItemsByText(search);
            foreach(Department item in items) subjectTable.Items.Add(new Department(item.id, item.name, item.description, item.faculty));
        }

        private void subjectTable_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (subjectTable.SelectedIndex >= 0)
            {
                var selectedItem = (dynamic)subjectTable.SelectedItem;
                nameField.Text = selectedItem.name;
                descriptionField.Document.Blocks.Add(new Paragraph(new Run(selectedItem.description)));
                facultyChoice.SelectedItem = selectedItem.faculty;
                id = selectedItem.id;
            }
        }

        private void subjectTable_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            CleanFields();
        }
    }
}
