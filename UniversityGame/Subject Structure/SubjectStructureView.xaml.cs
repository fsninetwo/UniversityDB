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

namespace UniversityGame.Subject_Structure
{
    /// <summary>
    /// Interaction logic for SubjectStructureView.xaml
    /// </summary>
    public partial class SubjectStructureView : UserControl
    {
        private DAO<SubjectStructure> dao;
        private int id = 0;
        private string search;
        public SubjectStructureView()
        {
            InitializeComponent();
            dao = new SubjectStructureDAO();
            Initialize("");
        }

        public void Initialize(string text)
        {
            search = text;
            AddFromDatabase();
            AddToClassformChoice();
            AddToSubjectChoice();
        }

        private bool checkColumns()
        {
            if (!Regex.IsMatch(countField.Text, "\\d+"))
            {
                MessageBox.Show("Count field must have a numeric value!", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (classformChoice.SelectedItem == null)
            {
                MessageBox.Show("Group box must have a value!", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (subjectChoice.SelectedItem == null)
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
                dao.Insert(new SubjectStructure(Convert.ToInt32(countField.Text), classformChoice.SelectedItem.ToString(), subjectChoice.SelectedItem.ToString()));
                AddFromDatabase();
            }
            CleanFields();
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            dao.DeleteById((dynamic)perfomanceTable.SelectedItem);
            AddFromDatabase();
        }

        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            if (checkColumns() && id != 0)
            {
                dao.UpdateById(new SubjectStructure(id, Convert.ToInt32(countField.Text), classformChoice.SelectedItem.ToString(), subjectChoice.SelectedItem.ToString()));
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
                for (int i = 0; i < perfomanceTable.Items.Count; i++)
                {
                    perfomanceTable.SelectedIndex = i;
                    SubjectStructure selectedItem = (dynamic)perfomanceTable.SelectedItem;
                    ws.Cells[i + 1, 1] = selectedItem.id.ToString();
                    ws.Cells[i + 1, 2] = selectedItem.count.ToString();
                    ws.Cells[i + 1, 3] = selectedItem.classform.ToString();
                    ws.Cells[i + 1, 4] = selectedItem.subject.ToString();
                }
                wb.SaveAs(sfd.FileName, XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing, false, false, XlSaveAsAccessMode.xlNoChange, XlSaveConflictResolution.xlLocalSessionChanges, Type.Missing, Type.Missing, true, false);
                app.Quit();
            }
        }

        public void AddToClassformChoice()
        {
            classformChoice.Items.Clear();
            List<string> items = dao.SelectToComboBox("classform");
            foreach (string item in items) classformChoice.Items.Add(item);
        }

        public void AddToSubjectChoice()
        {
            subjectChoice.Items.Clear();
            List<string> items = dao.SelectToComboBox("subject");
            foreach (string item in items) subjectChoice.Items.Add(item);
        }

        public void CleanFields()
        {
            countField.Text = "";
            classformChoice.SelectedItem = null;
            subjectChoice.SelectedItem = null;
            id = 0;
        }

        private void AddFromDatabase()
        {
            perfomanceTable.Items.Clear();
            List<SubjectStructure> items;
            if (search.Equals("")) items = dao.SelectItems();
            else items = dao.SelectItemsByText(search);
            foreach (SubjectStructure item in items) perfomanceTable.Items.Add(item);
        }

        private void subjectTable_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (perfomanceTable.SelectedIndex >= 0)
            {
                var selectedItem = (dynamic)perfomanceTable.SelectedItem;
                countField.Text = selectedItem.count.ToString();
                classformChoice.SelectedItem = selectedItem.classform;
                subjectChoice.SelectedItem = selectedItem.subject;
                id = selectedItem.id;
            }
        }

        private void subjectTable_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            CleanFields();
        }
    }
}
