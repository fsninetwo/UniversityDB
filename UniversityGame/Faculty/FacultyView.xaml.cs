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

namespace UniversityGame.Faculty
{
    /// <summary>
    /// Interaction logic for SubjectView.xaml
    /// </summary>
    public partial class FacultyView : UserControl
    {
        private DAO<Faculty> dao;
        private int id = 0;
        private string search;
        public FacultyView()
        {
            InitializeComponent();
            dao = new FacultyDAO();
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
                MessageBox.Show("Nickname field must have letters, numbers and doesn't have more than 50 symbols!", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (!Regex.IsMatch(new TextRange(descriptionField.Document.ContentStart, descriptionField.Document.ContentEnd).Text, "[\\w ]{1,5000}"))
            {
                MessageBox.Show("Nickname field must have letters, numbers and doesn't have more than 5000 symbols!", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }
        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            if (checkColumns())
            {
                dao.Insert(new Faculty(nameField.Text, new TextRange(descriptionField.Document.ContentStart, descriptionField.Document.ContentEnd).Text));
                AddFromDatabase();
            }
            CleanFields();
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            dao.DeleteById((dynamic)facultyTable.SelectedItem);
            AddFromDatabase();
        }

        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            if (checkColumns() && id != 0)
            {
                dao.UpdateById(new Faculty(id, nameField.Text, new TextRange(descriptionField.Document.ContentStart, descriptionField.Document.ContentEnd).Text));
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
                for (int i = 0; i < facultyTable.Items.Count; i++)
                {
                    facultyTable.SelectedIndex = i;
                    Faculty selectedItem = (dynamic)facultyTable.SelectedItem;
                    ws.Cells[i + 1, 1] = selectedItem.id.ToString();
                    ws.Cells[i + 1, 2] = selectedItem.name.ToString();
                    ws.Cells[i + 1, 3] = selectedItem.description.ToString();
                }
                wb.SaveAs(sfd.FileName, XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing, false, false, XlSaveAsAccessMode.xlNoChange, XlSaveConflictResolution.xlLocalSessionChanges, Type.Missing, Type.Missing, true, false);
                app.Quit();
            }
        }

        public void CleanFields()
        {
            nameField.Text = "";
            descriptionField.Document.Blocks.Add(new Paragraph(new Run(null)));
            id = 0;
        }

        private void AddFromDatabase()
        {
            facultyTable.Items.Clear();
            List<Faculty> items;
            if (search.Equals("")) items = dao.SelectItems();
            else items = dao.SelectItemsByText(search);
            foreach (Faculty item in items) facultyTable.Items.Add(item);
        }

        private void subjectTable_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (facultyTable.SelectedIndex >= 0)
            {
                var selectedItem = (dynamic)facultyTable.SelectedItem;
                nameField.Text = selectedItem.name;
                descriptionField.Document.Blocks.Add(new Paragraph(new Run(selectedItem.description)));
                id = selectedItem.id;
            }
        }

        private void subjectTable_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            CleanFields();
        }
    }
}
