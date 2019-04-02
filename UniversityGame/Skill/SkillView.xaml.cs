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

namespace UniversityGame.Skill
{
    /// <summary>
    /// Interaction logic for SkillView.xaml
    /// </summary>
    public partial class SkillView : UserControl
    {
        private DAO<Skill> dao;
        private int id = 0;
        private string search;
        public SkillView()
        {
            InitializeComponent();
            dao = new SkillDAO();
            Initialize("");
        }

        public void Initialize(string text)
        {
            search = text;
            AddFromDatabase();
            AddToCharacterChoice();
            AddToSubjectChoice();
        }

        private bool checkColumns()
        {
            if (!Regex.IsMatch(conditionField.Text, "\\d+"))
            {
                MessageBox.Show("Condition field must have a numeric value!", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (characterChoice.SelectedItem == null)
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
                dao.Insert(new Skill(Convert.ToInt32(conditionField.Text), characterChoice.SelectedItem.ToString(), subjectChoice.SelectedItem.ToString()));
                AddFromDatabase();
            }
            CleanFields();
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            dao.DeleteById((dynamic)skillTable.SelectedItem);
            AddFromDatabase();
        }

        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            if (checkColumns() && id != 0)
            {
                dao.UpdateById(new Skill(id, Convert.ToInt32(conditionField.Text), characterChoice.SelectedItem.ToString(), subjectChoice.SelectedItem.ToString()));
                AddFromDatabase();
            }
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
                for (int i = 0; i < skillTable.Items.Count; i++)
                {
                    skillTable.SelectedIndex = i;
                    Skill selectedItem = (dynamic)skillTable.SelectedItem;
                    ws.Cells[i + 1, 1] = selectedItem.id.ToString();
                    ws.Cells[i + 1, 2] = selectedItem.condition.ToString();
                    ws.Cells[i + 1, 3] = selectedItem.character.ToString();
                    ws.Cells[i + 1, 4] = selectedItem.subject.ToString();
                }
                wb.SaveAs(sfd.FileName, XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing, false, false, XlSaveAsAccessMode.xlNoChange, XlSaveConflictResolution.xlLocalSessionChanges, Type.Missing, Type.Missing, true, false);
                app.Quit();
            }
        }

        public void AddToCharacterChoice()
        {
            characterChoice.Items.Clear();
            List<string> items = dao.SelectToComboBox("character");
            foreach (string item in items) characterChoice.Items.Add(item);
        }

        public void AddToSubjectChoice()
        {
            subjectChoice.Items.Clear();
            List<string> items = dao.SelectToComboBox("subject");
            foreach (string item in items) subjectChoice.Items.Add(item);
        }

        public void CleanFields()
        {
            conditionField.Text = "";
            characterChoice.SelectedItem = null;
            subjectChoice.SelectedItem = null;
            id = 0;
        }

        private void AddFromDatabase()
        {
            skillTable.Items.Clear();
            List<Skill> items;
            if (search.Equals("")) items = dao.SelectItems();
            else items = dao.SelectItemsByText(search);
            foreach (Skill item in items) skillTable.Items.Add(item);
        }

        private void subjectTable_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (skillTable.SelectedIndex >= 0)
            {
                var selectedItem = (dynamic)skillTable.SelectedItem;
                conditionField.Text = selectedItem.condition.ToString();
                characterChoice.SelectedItem = selectedItem.character;
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
