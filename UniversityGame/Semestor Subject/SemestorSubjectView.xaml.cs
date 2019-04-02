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

namespace UniversityGame.Semestor_Subject
{
    /// <summary>
    /// Interaction logic for SemestorSubjectView.xaml
    /// </summary>
    public partial class SemestorSubjectView : UserControl
    {
        private DAO<SemestorSubject> dao;
        private int id = 0;
        private string search;
        public SemestorSubjectView()
        {
            InitializeComponent();
            dao = new SemestorSubjectDAO();
            Initialize("");
        }

        public void Initialize(string text)
        {
            search = text;
            AddFromDatabase();
            AddToGroupChoice();
        }

        private bool checkColumns()
        {
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
            if (!Regex.IsMatch(responseField.Text, "[\\w ]{1,30}"))
            {
                MessageBox.Show("Cabinet field must have letters, numbers and doesn't have more than 30 symbols!", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (!Regex.IsMatch(semestorField.Text, "\\d+"))
            {
                MessageBox.Show("Seestor field must have a numeric value!", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
                dao.Insert(new SemestorSubject(Convert.ToInt32(lectField.Text), Convert.ToInt32(practField.Text), Convert.ToInt32(labField.Text), responseField.Text, Convert.ToInt32(semestorField.Text), subjectChoice.SelectedItem.ToString()));
                AddFromDatabase();
            }
            CleanFields();
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            dao.DeleteById((dynamic)semestorSubjectTable.SelectedItem);
            AddFromDatabase();
        }

        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            if (checkColumns() && id != 0)
            {
                dao.UpdateById(new SemestorSubject(id, Convert.ToInt32(lectField.Text), Convert.ToInt32(practField.Text), Convert.ToInt32(labField.Text), responseField.Text, Convert.ToInt32(semestorField.Text), subjectChoice.SelectedItem.ToString()));
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
                for (int i = 0; i < semestorSubjectTable.Items.Count; i++)
                {
                    semestorSubjectTable.SelectedIndex = i;
                    SemestorSubject selectedItem = (dynamic)semestorSubjectTable.SelectedItem;
                    ws.Cells[i + 1, 1] = selectedItem.id.ToString();
                    ws.Cells[i + 1, 2] = selectedItem.lections.ToString();
                    ws.Cells[i + 1, 3] = selectedItem.practical.ToString();
                    ws.Cells[i + 1, 4] = selectedItem.labratory.ToString();
                    ws.Cells[i + 1, 5] = selectedItem.semestor.ToString();
                    ws.Cells[i + 1, 6] = selectedItem.subject.ToString();
                }
                wb.SaveAs(sfd.FileName, XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing, false, false, XlSaveAsAccessMode.xlNoChange, XlSaveConflictResolution.xlLocalSessionChanges, Type.Missing, Type.Missing, true, false);
                app.Quit();
            }
        }

        public void AddToGroupChoice()
        {
            subjectChoice.Items.Clear();
            List<string> items = dao.SelectToComboBox("subject");
            foreach (string item in items) subjectChoice.Items.Add(item);
        }

        public void CleanFields()
        {
            lectField.Text = "";
            practField.Text = "";
            labField.Text = "";
            responseField.Text = "";
            semestorField.Text = "";
            subjectChoice.SelectedItem = null;
            id = 0;
        }

        private void AddFromDatabase()
        {
            semestorSubjectTable.Items.Clear();
            List<SemestorSubject> items;
            if (search.Equals("")) items = dao.SelectItems();
            else items = dao.SelectItemsByText(search);
            foreach (SemestorSubject item in items) semestorSubjectTable.Items.Add(item);
        }

        private void subjectTable_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (semestorSubjectTable.SelectedIndex >= 0)
            {
                var selectedItem = (dynamic)semestorSubjectTable.SelectedItem;
                lectField.Text = selectedItem.lections.ToString();
                practField.Text = selectedItem.practical.ToString();
                labField.Text = selectedItem.labratory.ToString();
                responseField.Text = selectedItem.response;
                semestorField.Text = selectedItem.semestor.ToString();
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
