using Microsoft.Office.Interop.Excel;
using Microsoft.Win32;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Globalization;
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

namespace UniversityGame.Stress
{
    /// <summary>
    /// Interaction logic for StressView.xaml
    /// </summary>
    public partial class StressView : UserControl
    {
        private DAO<Stress> dao;
        private int id = 0;
        private string search;
        public StressView()
        {
            InitializeComponent();
            dao = new StressDAO();
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
            if (!Regex.IsMatch(dayField.ToString(), "(0[1-9]|[12]\\d|3[01])/(0[1-9]|1[0-2])/([12]\\d{3})"))
            {
                MessageBox.Show("Group box must have a date view Example:\"01/01/2000\"!", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (!Regex.IsMatch(stressField.ToString(), "(100|[1-9]\\d)|(\\d)+"))
            {
                MessageBox.Show("Stress field must have a value from 0 to 100!", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (characterChoice.SelectedItem == null)
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
                dao.Insert(new Stress(DateTime.ParseExact(dayField.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture).Date, Convert.ToInt32(stressField.Text), characterChoice.SelectedItem.ToString()));
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
                dao.UpdateById(new Stress(id, DateTime.ParseExact(dayField.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture).Date, Convert.ToInt32(stressField.Text), characterChoice.SelectedItem.ToString()));
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
                    Stress selectedItem = (dynamic)groupTable.SelectedItem;
                    ws.Cells[i + 1, 1] = selectedItem.id.ToString();
                    ws.Cells[i + 1, 2] = selectedItem.day.ToString();
                    ws.Cells[i + 1, 3] = selectedItem.stress.ToString();
                    ws.Cells[i + 1, 4] = selectedItem.nickname.ToString();
                }
                wb.SaveAs(sfd.FileName, XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing, false, false, XlSaveAsAccessMode.xlNoChange, XlSaveConflictResolution.xlLocalSessionChanges, Type.Missing, Type.Missing, true, false);
                app.Quit();
            }
        }
        public void AddToGroupChoice()
        {
            characterChoice.Items.Clear();
            List<string> items = dao.SelectToComboBox("character");
            foreach (string item in items) characterChoice.Items.Add(item);
        }

        public void CleanFields()
        {
            dayField.Text = "";
            stressField.Text = "";
            characterChoice.SelectedItem = null;
            id = 0;
        }

        private void AddFromDatabase()
        {
            groupTable.Items.Clear();
            List<Stress> items;
            if (search.Equals("")) items = dao.SelectItems();
            else items = dao.SelectItemsByText(search);
            foreach(Stress item in items) groupTable.Items.Add(item);
        }

        private void subjectTable_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (groupTable.SelectedIndex >= 0)
            {
                var selectedItem = (dynamic)groupTable.SelectedItem;
                dayField.Text = selectedItem.day.Date.ToString("dd/MM/yyyy");
                stressField.Text = selectedItem.stress.ToString();
                characterChoice.SelectedItem = selectedItem.nickname;
                id = selectedItem.id;
            }
        }

        private void subjectTable_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            CleanFields();
        }
    }
}
