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

namespace UniversityGame.Schedule
{
    /// <summary>
    /// Interaction logic for ScheduleView.xaml
    /// </summary>
    public partial class ScheduleView : UserControl
    {
        private DAO<Schedule> dao;
        private int id = 0;
        private string search;
        public ScheduleView()
        {
            InitializeComponent();
            dao = new ScheduleDAO();
            Initialize("");
        }

        public void Initialize(string text)
        {
            search = text;
            AddFromDatabase();
            AddToGroupChoice();
            AddToSubjectChoice();
        }

        private bool checkColumns()
        {
            if (!Regex.IsMatch(dayField.Text, "[1-7]"))
            {
                MessageBox.Show("Day field must have a value from 1 to 7!", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (!Regex.IsMatch(startField.Text, "^([0-1]?[0-9]|2[0-3]):[0-5][0-9]$"))
            {
                MessageBox.Show("Start field must have a classic time value!", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (!Regex.IsMatch(finishField.Text, "^([0-1]?[0-9]|2[0-3]):[0-5][0-9]$"))
            {
                MessageBox.Show("Finish field must have a classic time value!", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (!Regex.IsMatch(cabinetField.Text, "[\\w]{1,5}"))
            {
                MessageBox.Show("Cabinet field must have letters, numbers and doesn't have more than 5 symbols!", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (groupChoice.SelectedItem == null)
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
                dao.Insert(new Schedule(Convert.ToInt32(dayField.Text), DateTime.ParseExact(startField.Text, "HH:mm", CultureInfo.InvariantCulture).TimeOfDay, DateTime.ParseExact(finishField.Text, "HH:mm", CultureInfo.InvariantCulture).TimeOfDay, cabinetField.Text, groupChoice.SelectedItem.ToString(), subjectChoice.SelectedItem.ToString()));
                AddFromDatabase();
            }
            CleanFields();
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            dao.DeleteById((dynamic)scheduleTable.SelectedItem);
            AddFromDatabase();
        }

        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            if (checkColumns() && id != 0)
            {
                dao.UpdateById(new Schedule(id, Convert.ToInt32(dayField.Text), DateTime.ParseExact(startField.Text, "HH:mm", CultureInfo.InvariantCulture).TimeOfDay, DateTime.ParseExact(finishField.Text, "HH:mm", CultureInfo.InvariantCulture).TimeOfDay, cabinetField.Text, groupChoice.SelectedItem.ToString(), subjectChoice.SelectedItem.ToString()));
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
                for (int i = 0; i < scheduleTable.Items.Count; i++)
                {
                    scheduleTable.SelectedIndex = i;
                    Schedule selectedItem = (dynamic)scheduleTable.SelectedItem;
                    ws.Cells[i + 1, 1] = selectedItem.id.ToString();
                    ws.Cells[i + 1, 2] = selectedItem.day.ToString();
                    ws.Cells[i + 1, 3] = selectedItem.start.ToString();
                    ws.Cells[i + 1, 4] = selectedItem.finish.ToString();
                    ws.Cells[i + 1, 5] = selectedItem.cabinet.ToString();
                    ws.Cells[i + 1, 6] = selectedItem.group.ToString();
                }
                wb.SaveAs(sfd.FileName, XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing, false, false, XlSaveAsAccessMode.xlNoChange, XlSaveConflictResolution.xlLocalSessionChanges, Type.Missing, Type.Missing, true, false);
                app.Quit();
            }
        }

        public void AddToGroupChoice()
        {
            groupChoice.Items.Clear();
            List<string> items = dao.SelectToComboBox("group");
            foreach (string item in items) groupChoice.Items.Add(item);
        }

        public void AddToSubjectChoice()
        {
            subjectChoice.Items.Clear();
            List<string> items = dao.SelectToComboBox("subject");
            foreach (string item in items) subjectChoice.Items.Add(item);
        }

        public void CleanFields()
        {
            dayField.Text = "";
            startField.Text = "";
            finishField.Text = "";
            cabinetField.Text = "";
            groupChoice.SelectedItem = null;
            subjectChoice.SelectedItem = null;
            id = 0;
        }

        private void AddFromDatabase()
        {
            scheduleTable.Items.Clear();
            List<Schedule> items;
            if (search.Equals("")) items = dao.SelectItems();
            else items = dao.SelectItemsByText(search);
            foreach (Schedule item in items) scheduleTable.Items.Add(item);
        }

        private void subjectTable_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (scheduleTable.SelectedIndex >= 0)
            {
                var selectedItem = (dynamic)scheduleTable.SelectedItem;
                dayField.Text = selectedItem.day.ToString();
                startField.Text = selectedItem.start.ToString(@"hh\:mm");
                finishField.Text = selectedItem.finish.ToString(@"hh\:mm");
                cabinetField.Text = selectedItem.cabinet;
                groupChoice.SelectedItem = selectedItem.group;
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
